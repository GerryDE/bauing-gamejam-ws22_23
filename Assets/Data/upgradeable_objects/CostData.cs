using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Assets/Data/upgradeable_objects/CostData", menuName = "Data/CostData")]
    public class CostData : ScriptableObject
    {
        public int lumberCost;
        public int stoneCost;
    }
}