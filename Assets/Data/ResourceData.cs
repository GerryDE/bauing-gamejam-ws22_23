using System;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Assets/Data/ResourceData", menuName = "Data/ResourceData", order = 0)]
    public class ResourceData : ScriptableObject
    {
        [SerializeField] private int initialWoodAmount = 0;
        [SerializeField] private int initialStoneAmount = 0;
        
        
        [NonSerialized] public int CurrentWoodAmount;
        [NonSerialized] public int CurrentStoneAmount;

        private void Awake()
        {
            CurrentWoodAmount = initialWoodAmount;
            CurrentStoneAmount = initialStoneAmount;
        }
    }
}