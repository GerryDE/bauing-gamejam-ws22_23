using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Assets/Data/ResourceData", menuName = "Data/ResourceData", order = 0)]
    public class ResourceData : ScriptableObject
    {
        [SerializeField] private int initialWoodAmount;
        [SerializeField] private int initialStoneAmount;


        private int _currentWoodAmount;
        private int _currentStoneAmount;

        public int CurrentWoodAmount
        {
            get => _currentWoodAmount;
            set
            {
                _currentWoodAmount = value;
                OnCurrentWoodAmountChanged.Invoke(value);
            }
        }

        public int CurrentStoneAmount
        {
            get => _currentStoneAmount;
            set
            {
                _currentStoneAmount = value;
                OnCurrentStoneAmountChanged.Invoke(value);
            }
        }

        public delegate void CurrentWoodAmountChanged(int value);

        public delegate void CurrentStoneAmountChanged(int value);

        public static CurrentWoodAmountChanged OnCurrentWoodAmountChanged;
        public static CurrentStoneAmountChanged OnCurrentStoneAmountChanged;


        private void OnValidate()
        {
            _currentWoodAmount = initialWoodAmount;
            _currentStoneAmount = initialStoneAmount;
        }

        private void Awake()
        {
            OnCurrentWoodAmountChanged.Invoke(_currentWoodAmount);
            OnCurrentStoneAmountChanged.Invoke(_currentStoneAmount);
        }
    }
}
