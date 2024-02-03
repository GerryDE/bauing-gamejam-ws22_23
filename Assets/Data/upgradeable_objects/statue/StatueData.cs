using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Data
{
    [CreateAssetMenu(fileName = "Assets/Data/upgradeable_objects/statue/StatueData", menuName = "Data/StatueData")]
    public class StatueData : ScriptableObject
    {
        public enum UpgradeableStat
        {
            MaxHp,
            Atk,
            Def,
            Speed
        }

        public CostData baseUpgradeCost;
        public CostData upgradeCost;
        public float lumberCostMultiplier = 1.5f;
        public float stoneCostMultiplier = 1.5f;

        public List<UpgradeableStat> upgradeableStats;
        public int baseMaxHpValue = 50;
        public int baseAtkValue = 1;
        public int baseDefValue = 1;
        public int baseSpeedValue = 25;
        public float statMultiplier = 1.5f;

        public UpgradeableStat statToUpgrade;
        public int statValue;

        public UpgradeableStat GetRandomUpgradeableStat()
        {
            return upgradeableStats[Random.Range(0, upgradeableStats.Count)];
        }

        public void SetStatValue(int x)
        {
            statValue = (int) GetBaseValue() * (int) Math.Pow(x, statMultiplier);
        }

        private float GetBaseValue()
        {
            switch (statToUpgrade)
            {
                case UpgradeableStat.MaxHp:
                    return baseMaxHpValue;
                case UpgradeableStat.Atk:
                    return baseAtkValue;
                case UpgradeableStat.Def:
                    return baseDefValue;
                case UpgradeableStat.Speed:
                    return baseSpeedValue;
                default: return 0f;
            }
        }

        public StatueData Copy()
        {
            var copy = CreateInstance<StatueData>();
            copy.baseUpgradeCost = baseUpgradeCost;
            copy.upgradeCost = upgradeCost;
            copy.lumberCostMultiplier = lumberCostMultiplier;
            copy.stoneCostMultiplier = stoneCostMultiplier;
            copy.upgradeableStats = upgradeableStats;
            copy.statToUpgrade = GetRandomUpgradeableStat();
            copy.baseMaxHpValue = baseMaxHpValue;
            copy.baseAtkValue = baseAtkValue;
            copy.baseDefValue = baseDefValue;
            copy.baseSpeedValue = baseSpeedValue;
            copy.statMultiplier = statMultiplier;
            copy.statValue = statValue;
            return copy;
        }
    }
}