using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Assets/Data/upgradeable_objects/mine/MineData", menuName = "Data/MineData")]
    public class MineData : ScriptableObject
    {
        public CostData upgradeCost;
        public float miningDuaration = 1f;
        public int dropAmount = 1;
        public Sprite sprite;
    }
}