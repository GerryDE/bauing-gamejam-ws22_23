using System;
using System.Collections.Generic;
using AssemblyCSharp.Assets.Scripts;
using Data;
using Data.objective;
using Data.upgradeable_objects.statue;
using UnityEngine;

public class DataProvider : MonoBehaviour
{
    // Make sure that this component only exists once in the project to the keep the Singleton approach
    public static DataProvider Instance { get; private set; }

    [SerializeField] public PlayerData initialCurrentPlayerData;
    [SerializeField] private ResourceData initialResourceData;

    [SerializeField] private List<FenceDataIndex> fenceData;
    [SerializeField] private List<TreeData> treeData;
    [SerializeField] private List<MineData> mineData;
    [SerializeField] private StatueData initialStatueData;

    [SerializeField] private List<ObjectiveData> tutorialObjectives;
    [SerializeField] private List<ObjectiveData> dynamicObjectives;

    [NonSerialized] public CurrentPlayerData PlayerData;
    [NonSerialized] public CurrentResourceData InitialResourceData;
    [NonSerialized] public CurrentResourceData ResourceData;

    [NonSerialized] public List<FenceDataIndex> FenceData;
    [NonSerialized] public List<TreeData> TreeData;
    [NonSerialized] public List<MineData> MineData;
    [NonSerialized] public StatueData CurrentStatueData;
    [NonSerialized] public StatueData NextStatueData;

    [NonSerialized] public List<ObjectiveData> TutorialObjectives;
    [NonSerialized] public List<ObjectiveData> DynamicObjectives;

    public delegate void MaxRemainingYearsChanged(int value);

    public delegate void CurrentRemainingYearsChanged(int value);

    public delegate void AttackValueChanged(int value);

    public delegate void DefenseValueChanged(int value);

    public delegate void MoveSpeedChanged(float value);

    public delegate void RemainingYearsForStayingYoungChanged(int value);

    public delegate void RemainingYearsBecomingOldChanged(int value);

    public delegate void MinSpeedPercentageChanged(float value);

    public delegate void ThrowForceChanged(Vector2 value);

    public delegate void WoodAmountChanged(int value);

    public delegate void StoneAmountChanged(int value);

    public delegate void ResourceDataChanged(CurrentResourceData data);

    public delegate void FenceVersionChanged(int index, int newVersion);

    public delegate void TreeVersionChanged(int newVersion);

    public delegate void MineVersionChanged(int newVersion);

    public delegate void StatueVersionChanged(int newVersion);

    public delegate void TutorialObjectiveIndexChanged(int newIndex);

    public delegate void WaveCountChanged(int newWaveCount);

    public static MaxRemainingYearsChanged OnPlayerMaxRemainingYearsChanged;
    public static CurrentRemainingYearsChanged OnCurrentRemainingYearsChanged;
    public static AttackValueChanged OnAttackValueChanged;
    public static DefenseValueChanged OnDefenseValueChanged;
    public static MoveSpeedChanged OnMoveSpeedChanged;
    public static RemainingYearsForStayingYoungChanged OnRemainingYearsForStayingYoungChanged;
    public static RemainingYearsBecomingOldChanged OnRemainingYearsBecomingOldChanged;
    public static MinSpeedPercentageChanged OnMinSpeedPercentageChanged;
    public static ThrowForceChanged OnThrowForceChanged;
    public static WoodAmountChanged OnWoodAmountChanged;
    public static StoneAmountChanged OnStoneAmountChanged;
    public static ResourceDataChanged OnResourceDataChanged;
    public static FenceVersionChanged OnFenceVersionChanged;
    public static TreeVersionChanged OnTreeVersionChanged;
    public static MineVersionChanged OnMineVersionChanged;
    public static StatueVersionChanged OnStatueVersionChanged;
    public static TutorialObjectiveIndexChanged OnTutorialObjectiveIndexChanged;
    public static WaveCountChanged OnWaveCountChanged;

    private int _currentFenceVersion;
    private int _currentTreeVersion;
    private int _currentMineVersion;
    private int _currentStatueVersion;
    private int _currentTutorialObjectiveIndex;
    private int _waveCount;

    public int GetCurrentFenceVersion(int index)
    {
        return FenceData[index].version;
    }

    public void SetCurrentFenceVersion(int index, int value)
    {
        fenceData[index].version = value;
        OnFenceVersionChanged?.Invoke(index, value);
    }

    public int CurrentTreeVersion
    {
        get => _currentTreeVersion;
        set
        {
            _currentTreeVersion = value;
            OnTreeVersionChanged?.Invoke(value);
        }
    }

    public int CurrentMineVersion
    {
        get => _currentMineVersion;
        set
        {
            _currentMineVersion = value;
            OnMineVersionChanged?.Invoke(value);
        }
    }

    public int CurrentStatueVersion
    {
        get => _currentStatueVersion;
        set
        {
            _currentStatueVersion = value;
            OnStatueVersionChanged?.Invoke(value);
        }
    }

    public int CurrentTutorialObjectiveIndex
    {
        get => _currentTutorialObjectiveIndex;
        set
        {
            _currentTutorialObjectiveIndex = value;
            OnTutorialObjectiveIndexChanged?.Invoke(value);
        }
    }

    public int Wave
    {
        get => _waveCount;
        set
        {
            _waveCount = value;
            OnWaveCountChanged?.Invoke(_waveCount);
        }
    }
    
    public class CurrentPlayerData
    {
        private int _maxRemainingYears;
        private int _currentRemainingYears;
        private int _attackValue;
        private int _defenseValue;
        private float _moveSpeed;
        private int _remainingYearsForStayingYoung;
        private int _remainingYearsUntilBecomingOld;
        private float _minSpeedPercentage;
        private Vector2 _throwForce;

        public int MaxRemainingYears
        {
            get => _maxRemainingYears;
            set
            {
                _currentRemainingYears += value - _maxRemainingYears;
                _maxRemainingYears = value;
                OnCurrentRemainingYearsChanged?.Invoke(value);
                OnPlayerMaxRemainingYearsChanged?.Invoke(value);
            }
        }

        public int CurrentRemainingYears
        {
            get => _currentRemainingYears;
            set
            {
                _currentRemainingYears = Math.Clamp(value, 0, _maxRemainingYears);
                OnCurrentRemainingYearsChanged?.Invoke(value);
            }
        }

        public int AttackValue
        {
            get => _attackValue;
            set
            {
                _attackValue = value;
                OnAttackValueChanged?.Invoke(value);
            }
        }

        public int DefenseValue
        {
            get => _defenseValue;
            set
            {
                _defenseValue = value;
                OnDefenseValueChanged?.Invoke(value);
            }
        }

        public float MoveSpeed
        {
            get => _moveSpeed;
            set
            {
                _moveSpeed = value;
                OnMoveSpeedChanged?.Invoke(_moveSpeed);
            }
        }

        public int RemainingYearsForStayingYoung
        {
            get => _remainingYearsForStayingYoung;
            set
            {
                _remainingYearsForStayingYoung = value;
                OnRemainingYearsForStayingYoungChanged?.Invoke(value);
            }
        }

        public int RemainingYearsUntilBecomingOld
        {
            get => _remainingYearsUntilBecomingOld;
            set
            {
                _remainingYearsUntilBecomingOld = value;
                OnRemainingYearsBecomingOldChanged?.Invoke(value);
            }
        }

        public float MinSpeedPercentage
        {
            get => _minSpeedPercentage;
            set
            {
                _minSpeedPercentage = value;
                OnMinSpeedPercentageChanged?.Invoke(value);
            }
        }

        public Vector2 ThrowForce
        {
            get => _throwForce;
            set
            {
                _throwForce = value;
                OnThrowForceChanged?.Invoke(value);
            }
        }
    }

    public class CurrentResourceData
    {
        private int _woodAmount;
        private int _stoneAmount;

        public int WoodAmount
        {
            get => _woodAmount;
            set
            {
                _woodAmount = value;
                OnWoodAmountChanged?.Invoke(value);
                OnResourceDataChanged?.Invoke(this);
            }
        }

        public int StoneAmount
        {
            get => _stoneAmount;
            set
            {
                _stoneAmount = value;
                OnStoneAmountChanged?.Invoke(value);
                OnResourceDataChanged?.Invoke(this);
            }
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        PlayerData = new CurrentPlayerData
        {
            MaxRemainingYears = initialCurrentPlayerData.maxRemainingYears,
            CurrentRemainingYears = initialCurrentPlayerData.currentRemainingYears,
            AttackValue = initialCurrentPlayerData.attack,
            DefenseValue = initialCurrentPlayerData.defense,
            MoveSpeed = initialCurrentPlayerData.moveSpeed,
            RemainingYearsForStayingYoung = initialCurrentPlayerData.remainingYearsForStayingYoung,
            RemainingYearsUntilBecomingOld = initialCurrentPlayerData.remainingYearsForBecomingOld,
            MinSpeedPercentage = initialCurrentPlayerData.minSpeedPercentage,
            ThrowForce = initialCurrentPlayerData.throwBackForce
        };

        InitialResourceData = new CurrentResourceData
        {
            WoodAmount = initialResourceData.woodAmount,
            StoneAmount = initialResourceData.stoneAmount
        };

        ResourceData = new CurrentResourceData
        {
            WoodAmount = initialResourceData.woodAmount,
            StoneAmount = initialResourceData.stoneAmount
        };

        FenceData = fenceData;
        TreeData = treeData;
        MineData = mineData;
        CurrentStatueData = initialStatueData.Copy();
        TutorialObjectives = tutorialObjectives;
        DynamicObjectives = dynamicObjectives;
    }

    public CostData GetCostData(Interactable interactable, int version)
    {
        switch (interactable)
        {
            case Interactable.Fence_0_Repair: return FenceData[0].data[version].repairCost;
            case Interactable.Fence_0_Upgrade: return FenceData[0].data[version].upgradeCost;
            case Interactable.Fence_1_Repair: return FenceData[1].data[version].repairCost;
            case Interactable.Fence_1_Upgrade: return FenceData[1].data[version].upgradeCost;
            case Interactable.Tree_Upgrade: return TreeData[version].upgradeCost;
            case Interactable.Stone_Upgrade: return MineData[version].upgradeCost;
            case Interactable.Statue_Upgrade: return NextStatueData.upgradeCost;
        }

        return null;
    }
}

[Serializable]
public class FenceDataIndex
{
    [SerializeField] public List<FenceData> data;
    [SerializeField] public int version;
}