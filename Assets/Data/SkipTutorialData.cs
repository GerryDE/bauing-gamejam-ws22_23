using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Assets/Data/SkipTutorialData", menuName = "Data/SkipTutorialData", order = 0)]
    public class SkipTutorialData : ScriptableObject
    {
        public bool shallBeSkipped;
    }
}