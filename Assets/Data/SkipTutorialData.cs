using UnityEngine;
using UnityEngine.UIElements.Experimental;

namespace Data
{
    [CreateAssetMenu(fileName = "Assets/Data/SkipTutorialData", menuName = "Data/SkipTutorialData", order = 0)]
    public class SkipTutorialData : ScriptableObject
    {
        public delegate void SkipTutorialDataChanged(bool value);
        public static SkipTutorialDataChanged OnSkipTutorialDataChanged;

        [SerializeField] private bool _shallBeSkipped;

        public bool ShallBeSkipped
        {
            get => _shallBeSkipped;
            set
            {
                _shallBeSkipped = value;
                OnSkipTutorialDataChanged?.Invoke(value);
            }
        }
    }
}