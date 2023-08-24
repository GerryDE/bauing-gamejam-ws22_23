using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Assets/Data/upgradeable_objects/statue/StatueData", menuName = "Data/StatueData")]
    public class StatueData : ScriptableObject
    {
        public CostData upgradeCost;
        public int maxAge;
        public Sprite sprite;
    }
}