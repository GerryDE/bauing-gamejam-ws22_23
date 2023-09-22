using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Assets/Data/upgradeable_objects/fence/FenceData", menuName = "Data/FenceData")]
    public class FenceData : ScriptableObject
    {
        [Header("Upgrade")]
        public CostData upgradeCost;
        public int maxHp = 50;
        public int damage = 1;

        [Header("Repair")]
        public CostData repairCost;
        public int repairHealAmount = 25;

        [Header("Sprite")]
        public Sprite sprite;
    }
}