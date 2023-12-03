using System;
using System.Collections.Generic;
using System.Linq;
using Data.objective;
using Objective;
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

    private struct StateData
    {
        public int dropAmount;
        public float defaultStateChangeDuration;
        public float miningDuration;
        public Sprite sprite;
    }

    [Serializable]
    private struct Range
    {
        public float min, max;
    }

    [SerializeField] private Range spawnRange;
    [SerializeField] private State state = Spawning;
    private ProgressBarComponent _progressBarComponent;

    [Range(0f, 1f)] [SerializeField] private float stateChangeDurationVariance = 0.2f;

    private float _stateChangeDuration;
    private float _elapsedStateChangeTime;
    private float _elapsedMiningTime;
    private bool _allowGrowing = false;

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
        _renderer.sprite = GetDataByCurrentState()?.sprite;
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

        TutorialComponent.OnNewObjectiveStarted += OnNewObjectiveStarted;
    }

    private void OnNewObjectiveStarted(ObjectiveData data)
    {
        if(data.GetType() != typeof(CollectResourcesObjectiveData)) return;
        _allowGrowing = true;
        SetState(Large);
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
        if (!_allowGrowing) return;

        _elapsedStateChangeTime += Time.deltaTime;
        if (_elapsedStateChangeTime > _stateChangeDuration)
        {
            _elapsedStateChangeTime = 0;
            switch (state)
            {
                case Spawning: 
                    state = Small; 
                    break;
                case Small:
                    state = Medium; 
                    break;
                case Medium:
                    state = Large; 
                    break;
            };

            _renderer.sprite = GetDataByCurrentState()?.sprite;
            CalculateStateChangeDuration();
        }

        if (!Spawning.Equals(state) && _interaction1Enabled)
        {
            _elapsedMiningTime += Time.deltaTime;

            _progressBarComponent.Enable();

            var data = DataProvider.Instance;
            var treeData = data.TreeData;
            float miningDuration = 0f;
            switch (state)
            {
                case Spawning:
                    miningDuration = treeData[data.CurrentTreeVersion].spawningMiningDuration;
                    break;
                case Small:
                    miningDuration = treeData[data.CurrentTreeVersion].smallMiningDuration;
                    break;
                case Medium:
                    miningDuration = treeData[data.CurrentTreeVersion].mediumMiningDuration;
                    break;
                case Large:
                    miningDuration = treeData[data.CurrentTreeVersion].largeMiningDuration;
                    break;
            }

            var currentData = GetDataByCurrentState();

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
        var data = DataProvider.Instance;
        var treeData = data.TreeData;
        var currentTreeData = treeData[data.CurrentTreeVersion];

        Dictionary<State, StateData> stateDataDict = new Dictionary<State, StateData>
        {
            {
                Spawning,
                new StateData
                {
                    dropAmount = currentTreeData.spawingDropAmount,
                    defaultStateChangeDuration = currentTreeData.spawningDefaultStateChangeDuration,
                    miningDuration = currentTreeData.spawningMiningDuration,
                    sprite = currentTreeData.spawningSprite
                }
            },
            {
                Small,
                new StateData
                {
                    dropAmount = currentTreeData.smallDropAmount,
                    defaultStateChangeDuration = currentTreeData.smallDefaultStateChangeDuration,
                    miningDuration = currentTreeData.smallMiningDuration,
                    sprite = currentTreeData.smallSprite
                }
            },
            {
                Medium,
                new StateData
                {
                    dropAmount = currentTreeData.mediumDropAmount,
                    defaultStateChangeDuration = currentTreeData.mediumDefaultStateChangeDuration,
                    miningDuration = currentTreeData.mediumMiningDuration,
                    sprite = currentTreeData.mediumSprite
                }
            },
            {
                Large,
                new StateData
                {
                    dropAmount = currentTreeData.largeDropAmount,
                    defaultStateChangeDuration = currentTreeData.largeDefaultStateChangeDuration,
                    miningDuration = currentTreeData.largeMiningDuration,
                    sprite = currentTreeData.largeSprite
                }
            }
        };

        return stateDataDict[state];
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