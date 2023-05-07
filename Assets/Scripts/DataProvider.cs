using System;
using Data;
using UnityEngine;

public class DataProvider : MonoBehaviour
{
    // Make sure that this component only exists once in the project to the keep the Singleton approach
    public static DataProvider Instance { get; private set; }

    [SerializeField] private PlayerData initialCurrentPlayerData;
    [SerializeField] private ResourceData initialResourceData;

    public CurrentPlayerData PlayerData;
    public CurrentResourceData ResourceData;

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
                _maxRemainingYears = value;
                OnPlayerMaxRemainingYearsChanged?.Invoke(value);
            }
        }

        public int CurrentRemainingYears
        {
            get => _currentRemainingYears;
            set
            {
                _currentRemainingYears = Math.Max(0, value);
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
            }
        }

        public int StoneAmount
        {
            get => _stoneAmount;
            set
            {
                _stoneAmount = value;
                OnStoneAmountChanged?.Invoke(value);
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

        ResourceData = new CurrentResourceData
        {
            WoodAmount = initialResourceData.woodAmount,
            StoneAmount = initialResourceData.stoneAmount
        };
    }
}