using System;
using UnityEngine;

namespace Data.objective
{
    [CreateAssetMenu(fileName = "Assets/Data/Objective/RepairFenceObjectiveData", menuName = "Data/RepairFenceObjectiveData")]
    public class RepairFenceObjectiveData : ObjectiveData
    {
        public int triggerValueInPercent = 50;
        
        public override string GetObjectiveText()
        {
            return "Repair your fence";
        }
    }
}