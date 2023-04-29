using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Assets/Data/PlayerData", menuName = "Data/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private int initialMaxRemainingYears = 50;

        [SerializeField] private int initialAttack = 1;

        [SerializeField] private int initialDefense = 1;

        [SerializeField,
         Tooltip("The initial max move speed value when the Player has the maximum amount of remaining years")]
        private float initialMoveSpeed = 300f;

        [SerializeField,
         Tooltip("The initial amount of remaining years that is required to start transition from young to old")]
        private int initialRemainingYearsForStayingYoung = 40;

        [SerializeField,
         Tooltip("The initial amount of remaining years that is required to finish transition from young to old")]
        private int initialRemainingYearsForBecomingOld = 10;

        [SerializeField,
         Tooltip(
             "The initial minimal move speed percentage value after the transition to old has been finished. " +
             "For example, when move speed is 300 and the speed percentage value is 0.2, the Player's move speed " +
             "will be 60 after finishing the transition to become old.")]
        private float initialMinSpeedPercentage = 0.2f;

        [SerializeField,
         Tooltip("The force that shall be performed on the Player to throw it back after getting hit by an enemy.")]
        private Vector2 initialThrowBackForce = new Vector2(500f, 0f);

        public delegate void PlayerMaxRemainingYearsChanged(int value);

        public delegate void PlayerCurrentRemainingYearsChanged(int value);

        public delegate void PlayerAttackChanged(int value);

        public delegate void PlayerDefenseChanged(int value);

        public delegate void PlayerMoveSpeedChanged(float value);

        public delegate void PlayerRemainingYearsForStayingYoungChanged(int value);

        public delegate void PlayerRemainingYearsForBecomingOldChanged(int value);

        public delegate void PlayerMinSpeedPercentageChanged(float value);

        public delegate void PlayerThrowBackForceChanged(Vector2 value);

        public static PlayerMaxRemainingYearsChanged OnPlayerMaxRemainingYearsChanged;
        public static PlayerCurrentRemainingYearsChanged OnPlayerCurrentRemainingYearsChanged;
        public static PlayerAttackChanged OnPlayerAttackChanged;
        public static PlayerDefenseChanged OnPlayerDefenseChanged;
        public static PlayerMoveSpeedChanged OnPlayerMoveSpeedChanged;
        public static PlayerRemainingYearsForStayingYoungChanged OnPlayerRemainingYearsForStayingYoungChanged;
        public static PlayerRemainingYearsForBecomingOldChanged OnPlayerRemainingYearsForBecomingOldChanged;
        public static PlayerMinSpeedPercentageChanged OnPlayerMinSpeedPercentageChanged;
        public static PlayerThrowBackForceChanged OnPlayerThrowBackForceChanged;

        private int _maxRemainingYears;
        private int _currentRemainingYears;
        private int _attack;
        private int _defense;
        private float _moveSpeed;
        private int _remainingYearsForStayingYoung;
        private int _remainingYearsForBecomingOld;
        private float _minSpeedPercentage;
        private Vector2 _throwBackForce;

        public int MaxRemainingYears
        {
            get => _maxRemainingYears;
            set
            {
                _maxRemainingYears = value;
                OnPlayerMaxRemainingYearsChanged.Invoke(value);
            }
        }

        public int CurrentRemainingYears
        {
            get => _currentRemainingYears;
            set
            {
                _currentRemainingYears = value;
                OnPlayerCurrentRemainingYearsChanged.Invoke(value);
            }
        }

        public int Attack
        {
            get => _attack;
            set
            {
                _attack = value;
                OnPlayerAttackChanged.Invoke(value);
            }
        }

        public int Defense
        {
            get => _defense;
            set
            {
                _defense = value;
                OnPlayerDefenseChanged.Invoke(value);
            }
        }

        public float MoveSpeed
        {
            get => _moveSpeed;
            set
            {
                _moveSpeed = value;
                OnPlayerMoveSpeedChanged.Invoke(value);
            }
        }

        public int RemainingYearsForStayingYoung
        {
            get => _remainingYearsForStayingYoung;
            set
            {
                _remainingYearsForStayingYoung = value;
                OnPlayerRemainingYearsForStayingYoungChanged.Invoke(value);
            }
        }

        public int RemainingYearsForBecomingOld
        {
            get => _remainingYearsForBecomingOld;
            set
            {
                _remainingYearsForBecomingOld = value;
                OnPlayerRemainingYearsForBecomingOldChanged.Invoke(value);
            }
        }

        public float MinSpeedPercentage
        {
            get => _minSpeedPercentage;
            set
            {
                _minSpeedPercentage = value;
                OnPlayerMinSpeedPercentageChanged.Invoke(value);
            }
        }

        public Vector2 ThrowBackForce
        {
            get => _throwBackForce;
            set
            {
                _throwBackForce = value;
                OnPlayerThrowBackForceChanged.Invoke(value);
            }
        }

        private void OnValidate()
        {
            _maxRemainingYears = initialMaxRemainingYears;
            _currentRemainingYears = initialMaxRemainingYears;
            _attack = initialAttack;
            _defense = initialDefense;
            _moveSpeed = initialMoveSpeed;
            _remainingYearsForStayingYoung = initialRemainingYearsForStayingYoung;
            _remainingYearsForBecomingOld = initialRemainingYearsForBecomingOld;
            _minSpeedPercentage = initialMinSpeedPercentage;
            _throwBackForce = initialThrowBackForce;
        }

        private void Awake()
        {
            OnPlayerMaxRemainingYearsChanged.Invoke(_maxRemainingYears);
            OnPlayerCurrentRemainingYearsChanged.Invoke(_currentRemainingYears);
            OnPlayerAttackChanged.Invoke(_attack);
            OnPlayerDefenseChanged.Invoke(_defense);
            OnPlayerMoveSpeedChanged.Invoke(_moveSpeed);
            OnPlayerRemainingYearsForStayingYoungChanged.Invoke(_remainingYearsForStayingYoung);
            OnPlayerRemainingYearsForBecomingOldChanged.Invoke(_remainingYearsForBecomingOld);
            OnPlayerMinSpeedPercentageChanged.Invoke(_minSpeedPercentage);
            OnPlayerThrowBackForceChanged.Invoke(_throwBackForce);
        }
    }
}
