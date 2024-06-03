using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Assets/Data/upgradeable_objects/tree/TreeData", menuName = "Data/TreeData")]
    public class TreeData : ScriptableObject
    {
        [Header("Cost data")]
        public CostData upgradeCost;

        [Header("Spawing")]
        public int spawingDropAmount;
        public float spawningDefaultStateChangeDuration = 15;
        public float spawningMiningDuration;
        public Sprite spawningSprite;

        [Header("Small")]
        public int smallDropAmount = 1;
        public float smallDefaultStateChangeDuration = 15;
        public float smallMiningDuration = 1;
        public Sprite smallSprite;

        [Header("Medium")]
        public int mediumDropAmount = 3;
        public float mediumDefaultStateChangeDuration = 15;
        public float mediumMiningDuration = 2;
        public Sprite mediumSprite;

        [Header("Large")]
        public int largeDropAmount = 5;
        public float largeDefaultStateChangeDuration = 15;
        public float largeMiningDuration = 3;
        public Sprite largeSprite;
    }
}