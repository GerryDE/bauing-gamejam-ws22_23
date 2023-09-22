using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Assets/Data/upgradeable_objects/statue/StatueData", menuName = "Data/StatueData")]
    public class StatueData : ScriptableObject
    {
        public CostData upgradeCost;
        public int maxAge = 50;
        public float prayingDuration = 0.7f;
        public float prayingDurationMultiplicator = 0.9f;
        public Sprite sprite;
    }
}