using System;
using UnityEngine;

namespace Data.objective
{
    [CreateAssetMenu(fileName = "Assets/Data/Objective/CollectResourcesObjectiveData", menuName = "Data/CollectResourcesObjectiveData")]
    public class CollectResourcesObjectiveData : ObjectiveData
    {
        public int lumberAmount = 10;
        public int stoneAmount = 5;
        
        public override string GetObjectiveText()
        {
            if (lumberAmount > 0 && stoneAmount > 0)
            {
                return $"Collect {lumberAmount} lumber and {stoneAmount} stones";
            }
            if (lumberAmount > 0)
            {
                return $"Collect {lumberAmount} lumber";
            } 
            if (stoneAmount > 0)
            {
                return $"Collect {stoneAmount} stones";
            }
            
            Debug.LogError("Neither lumberAmount nor stoneAmount are higher than zero. One must be higher than zero!");
            return "";
        }
    }
}