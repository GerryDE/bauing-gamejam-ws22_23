using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Assets/Data/StatisticsData", menuName = "Data/StatisticsData", order = 0)]
    public class StatisticsData : ScriptableObject
    {
        public int LumberAmount;
        public int StoneAmount;
        public int PrayAmount;
        public int EnemyDestroyedAmount;
        public int SurvivedWavesAmount;
        public float PlayTime;
        public int CloudsScreenLeftAmount;
        public int FenceRepairAmount;
        public int UpgradeAmount;
    }
}