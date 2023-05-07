using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Assets/Data/ResourceData", menuName = "Data/ResourceData", order = 0)]
    public class ResourceData : ScriptableObject
    {
        public int woodAmount;
        public int stoneAmount;
    }
}