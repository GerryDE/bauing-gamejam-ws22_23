using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Assets/Data/upgradeable_objects/statue/LegacyStatueData", menuName = "Data/LegacyStatueData")]
    public class LegacyStatueData : ScriptableObject
    {
        public CostData upgradeCost;
        public int maxAge = 50;
        public float prayingDuration = 0.7f;
        public float prayingDurationMultiplicator = 0.9f;
        public Sprite sprite;
    }
}