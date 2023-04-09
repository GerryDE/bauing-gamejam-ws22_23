using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
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
        public float defaultStateChangeDuration;
        public float miningDuration;
        public Sprite sprite;
    }

    [Serializable]
    private struct VersionData
    {
        public List<StateData> data;
    }

    [Serializable]
    private struct Range
    {
        public float min, max;
    }

    [SerializeField] private Range spawnRange;
    [SerializeField] private State state = Spawning;
    [SerializeField] private List<VersionData> data;
    private ProgressBarComponent _progressBarComponent;

    [Range(0f, 1f)] [SerializeField] private float stateChangeDurationVariance = 0.2f;

    private float _stateChangeDuration;
    private float _elapsedStateChangeTime;
    private float _elapsedMiningTime;

    private SpriteRenderer _renderer;

    public State GetState()
    {
        return state;
    }

    public delegate void DropWood(int amount);

    public static DropWood OnDropWood;

    public void SetState(State newState)
    {
        state = newState;
        CalculateStateChangeDuration();
    }

    protected override void Start()
    {
        _progressBarComponent = GetComponent<ProgressBarComponent>();
        base.Start();
        
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = GetDataByCurrentState()?.sprite;

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
            
            _progressBarComponent.Enable();
            var currentData = GetDataByCurrentState();
            if (currentData == null) return;

            _progressBarComponent.UpdateValues(_elapsedMiningTime, currentData.Value.miningDuration);
            if (_elapsedMiningTime > currentData.Value.miningDuration)
            {
                _elapsedMiningTime = 0f;
                OnDropWood?.Invoke(currentData.Value.dropAmount);
                _dataHandlerComponent.PlayWoodCuttingAudioClip();
                Respawn();
                _progressBarComponent.Disable();
                _interaction1Enabled = false;
                _interactionButton1Pressed = false;
            }
        }
        else
        {
            _progressBarComponent.Disable();
            _elapsedMiningTime = 0f;
            _progressBarComponent.UpdateValues(_elapsedMiningTime, 1f);
        }
    }

    private StateData? GetDataByCurrentState()
    {
        foreach (var currentData in data[_dataHandlerComponent.CurrentTreeVersion].data
                     .Where(currentData => state.Equals(currentData.state)))
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
        var defaultStateChangeDuration = GetDataByCurrentState().Value.defaultStateChangeDuration;
        _stateChangeDuration = Random.Range(defaultStateChangeDuration * (1f - stateChangeDurationVariance),
            defaultStateChangeDuration * (1f + stateChangeDurationVariance));
    }
}