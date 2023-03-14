using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static TreeComponent.State;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SpriteRenderer))]
public class TreeComponent : InteractableBaseComponent
{
    public enum State
    {
        Spawning,
        Small,
        Medium,
        Large
    }

    [Serializable]
    private struct StateData
    {
        public State state;
        public int dropAmount;
        public int miningDuration;
        public Sprite sprite;
    }

    [Serializable]
    private struct Range
    {
        public float min, max;
    }

    [SerializeField] private Range spawnRange;
    [SerializeField] private State state = Spawning;
    [SerializeField] private List<StateData> data;

    [SerializeField] private float defaultStateChangeDuration = 5f;

    [SerializeField] private float stateChangeDurationVariance = 0.2f;

    private float _stateChangeDuration;
    private float _elapsedStateChangeTime;
    private float _elapsedMiningTime;

    private SpriteRenderer _renderer;

    public delegate void DropWood(int amount);

    public static DropWood OnDropWood;

    public void SetState(State newState)
    {
        state = newState;
        CalculateStateChangeDuration();
    }

    protected override void Start()
    {
        base.Start();

        _renderer = GetComponent<SpriteRenderer>();

        SetSpawnPosition();
        CalculateStateChangeDuration();
    }

    private void Respawn()
    {
        state = Spawning;
        _renderer.sprite = null;
        SetSpawnPosition();
        CalculateStateChangeDuration();
    }

    private void FixedUpdate()
    {
        // if (_isRespawning) return;

        _elapsedStateChangeTime += Time.deltaTime;
        if (_elapsedStateChangeTime > _stateChangeDuration)
        {
            _elapsedStateChangeTime = 0;
            state = state switch
            {
                Spawning => Small,
                Small => Medium,
                Medium => Large,
                _ => state
            };

            _renderer.sprite = GetDataByCurrentState()?.sprite;
            CalculateStateChangeDuration();
        }

        if (!Spawning.Equals(state) && _interaction1Enabled)
        {
            _elapsedMiningTime += Time.deltaTime;
            var currentData = GetDataByCurrentState();
            if (currentData.HasValue && _elapsedMiningTime > currentData.Value.miningDuration)
            {
                _elapsedMiningTime = 0f;
                OnDropWood?.Invoke(currentData.Value.dropAmount);
                Respawn();
            }
        }
        else
        {
            _elapsedMiningTime = 0f;
        }
    }

    private StateData? GetDataByCurrentState()
    {
        foreach (var currentData in data.Where(currentData => state.Equals(currentData.state)))
        {
            return currentData;
        }

        return null;
    }

    private void SetSpawnPosition()
    {
        var xPos = Random.Range(spawnRange.min, spawnRange.max);
        var yPos = transform.position.y;
        transform.position = new Vector3(xPos, yPos, 0f);
    }

    private void CalculateStateChangeDuration()
    {
        _stateChangeDuration = Random.Range(defaultStateChangeDuration * (1f - stateChangeDurationVariance),
            defaultStateChangeDuration * (1f + stateChangeDurationVariance));
    }
}