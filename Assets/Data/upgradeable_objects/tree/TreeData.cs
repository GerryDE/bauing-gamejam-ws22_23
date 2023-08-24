using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Assets/Data/upgradeable_objects/tree/TreeData", menuName = "Data/TreeData")]
    public class TreeData : ScriptableObject
    {
        //public int dropAmount;
        public CostData upgradeCost;
        //public Sprite sprite;
    }
}