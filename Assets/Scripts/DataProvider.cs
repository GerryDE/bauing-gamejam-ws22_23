using System;
using System.Collections.Generic;
using AssemblyCSharp.Assets.Scripts;
using Data;
using Data.EnemySpawning;
using Data.objective;
using Data.upgradeable_objects.statue;
using UnityEngine;

public class DataProvider : MonoBehaviour
{
    // Make sure that this component only exists once in the project to the keep the Singleton approach
    public static DataProvider Instance { get; private set; }

    [Header("Player related data")] [SerializeField]
    public PlayerData initialCurrentPlayerData;

    [SerializeField] private ResourceData initialResourceData;

    [Header("Interactable data")] [SerializeField]
    private List<FenceDataIndex> fenceData;

    [SerializeField] private List<TreeData> treeData;
    [SerializeField] private List<MineData> mineData;
    [SerializeField] private StatueData initialStatueData;

    [Header("Objective data")] [SerializeField]
    private List<ObjectiveData> tutorialObjectives;

    [SerializeField] private List<ObjectiveData> dynamicObjectives;

    [Header("Enemy wave data")] [SerializeField]
    private List<EnemySpawnWaveData> enemySpawnWaveData;

    [Header("Audio data")] [SerializeField]
    private AudioClip defaultBgm;
    [SerializeField] private AudioClip enemyBgm;
    [SerializeField] private AudioClip bossBgm;
    [SerializeField] private AudioClip finalBossBgm;

    [Header("Volume data")] [SerializeField]
    private AudioVolumeData volumeData;

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

    public AudioClip DefaultBgm => defaultBgm;
    public AudioClip EnemyBgm => enemyBgm;
    public AudioClip BossBgm => bossBgm;
    public AudioClip FinalBossBgm => finalBossBgm;
    
    public List<EnemySpawnWaveData> EnemySpawnWaveDataList => enemySpawnWaveData;

    public delegate void MaxRemainingYearsChanged(int value);

    public delegate void CurrentRemainingYearsChanged(int value);

    public delegate void AttackValueChanged(int value);

    public delegate void DefenseValueChanged(int value);

    public delegate void MoveSpeedChanged(float value);

    public delegate void MaxHpLevelChanged(int value);

    public delegate void AttackLevelChanged(int value);

    public delegate void DefenseLevelChanged(int value);

    public delegate void SpeedLevelChanged(int value);

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
    
    public delegate void SubWaveCountChanged(int newSubWaveCount);
    
    public delegate void UseAlternateLayoutChanged(bool useAlternateLayout);

    public delegate void VolumeDataChanged(AudioVolumeData volumeData);

    public static MaxRemainingYearsChanged OnPlayerMaxRemainingYearsChanged;
    public static CurrentRemainingYearsChanged OnCurrentRemainingYearsChanged;
    public static AttackValueChanged OnAttackValueChanged;
    public static DefenseValueChanged OnDefenseValueChanged;
    public static MoveSpeedChanged OnMoveSpeedChanged;

    public static MaxHpLevelChanged OnMaxHpLevelChanged;
    public static AttackLevelChanged OnAttackLevelChanged;
    public static DefenseLevelChanged OnDefenseLevelChanged;
    public static SpeedLevelChanged OnSpeedLevelChanged;

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
    public static SubWaveCountChanged OnSubWaveCountChanged;

    public static UseAlternateLayoutChanged OnUseAlternateLayoutChanged;

    public static VolumeDataChanged OnVolumeDataChanged;

    private int _currentFenceVersion;
    private int _currentTreeVersion;
    private int _currentMineVersion;
    private int _currentStatueVersion;
    private int _currentTutorialObjectiveIndex;
    private int _waveCount;
    private int _subWaveCount;
    private bool _useAlternateLayout;

    public AudioVolumeData VolumeData
    {
        get => volumeData;
        set
        {
            volumeData = value;
            volumeData.Volume = Mathf.Clamp(volumeData.Volume, 0f, 1f);
            OnVolumeDataChanged?.Invoke(value);
        }
    }

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

    public int WaveCount
    {
        get => _waveCount;
        set
        {
            _waveCount = value;
            OnWaveCountChanged?.Invoke(_waveCount);
        }
    }
    
    public int SubWaveCount
    {
        get => _subWaveCount;
        set
        {
            _subWaveCount = value;
            OnSubWaveCountChanged?.Invoke(_subWaveCount);
        }
    }

    public bool UseAlternateLayout
    {
        get => _useAlternateLayout;
        set
        {
            _useAlternateLayout = value;
            OnUseAlternateLayoutChanged?.Invoke(value);
        }
    }

    public EnemySpawnWaveData CurrentWaveData()
    {
        return enemySpawnWaveData[_waveCount];
    }
    
    public EnemySpawnSubWaveData CurrentSubWaveData()
    {
        return CurrentWaveData().subWaves[_subWaveCount];
    }

    public class SubWave
    {
        private int _count;
        private List<int> _assignedEnemyIds;
        
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                OnSubWaveCountChanged?.Invoke(value);
            }
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
        private int _maxHpLevel;
        private int _attackLevel;
        private int _defenseLevel;
        private int _speedLevel;

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

        public int MaxHpLevel
        {
            get => _maxHpLevel;
            set
            {
                _maxHpLevel = value;
                OnMaxHpLevelChanged?.Invoke(value);
            }
        }

        public int AttackLevel
        {
            get => _attackLevel;
            set
            {
                _attackLevel = value;
                OnAttackLevelChanged?.Invoke(value);
            }
        }

        public int DefenseLevel
        {
            get => _defenseLevel;
            set
            {
                _defenseLevel = value;
                OnDefenseLevelChanged?.Invoke(value);
            }
        }

        public int SpeedLevel
        {
            get => _speedLevel;
            set
            {
                _speedLevel = value;
                OnSpeedLevelChanged?.Invoke(value);
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
                _woodAmount = Math.Min(999, value);
                OnWoodAmountChanged?.Invoke(value);
                OnResourceDataChanged?.Invoke(this);
            }
        }

        public int StoneAmount
        {
            get => _stoneAmount;
            set
            {
                _stoneAmount = Math.Min(999, value);
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