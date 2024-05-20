using System;
using System.Collections.Generic;
using UnityEngine;
using static Data.upgradeable_objects.statue.StatueData.UpgradeableStat;
using Random = UnityEngine.Random;

namespace Data.upgradeable_objects.statue
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

        [Serializable]
        public struct Stat
        {
            public UpgradeableStat StatType;
            public int BaseValue;
            public float Multiplier;
            public int Version;

            public Stat(UpgradeableStat statType, int baseValue, float multiplier, int version)
            {
                StatType = statType;
                BaseValue = baseValue;
                Multiplier = multiplier;
                Version = version;
            }
        }

        public Stat maxHpStat;
        public Stat atkStat;
        public Stat defStat;
        public Stat speedStat;

        public CostData baseUpgradeCost;
        public CostData upgradeCost;
        public float lumberCostMultiplier = 1.5f;
        public float stoneCostMultiplier = 1.5f;

        public List<UpgradeableStat> upgradeableStats;

        public UpgradeableStat statToUpgrade;
        public int statValue;

        private UpgradeableStat GetRandomUpgradeableStat()
        {
            return upgradeableStats[Random.Range(0, upgradeableStats.Count)];
        }

        public void SetStatValue(int x)
        {
            statValue = (int)(GetBaseValue() * Mathf.Pow(GetStat().Multiplier, x));
            Debug.Log("Value will change to " + statValue);
        }

        private int GetBaseValue()
        {
            return GetStat().BaseValue;
        }

        private Stat GetStat()
        {
            return statToUpgrade switch
            {
                MaxHp => maxHpStat,
                Atk => atkStat,
                Def => defStat,
                Speed => speedStat,
                _ => throw new ArgumentOutOfRangeException()
            };
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
            copy.maxHpStat = maxHpStat;
            copy.atkStat = atkStat;
            copy.defStat = defStat;
            copy.speedStat = speedStat;
            copy.statValue = statValue;
            return copy;
        }
    }
}