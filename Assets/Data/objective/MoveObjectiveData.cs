using System;
using UnityEngine;

namespace Data.objective
{
    [CreateAssetMenu(fileName = "Assets/Data/Objective/MoveObjectiveData", menuName = "Data/MoveObjectiveData")]
    public class MoveObjectiveData : ObjectiveData
    {
        public bool shallMoveLeft = true;
        public bool shallMoveRight = true;
        
        public override string GetObjectiveText()
        {
            if (shallMoveLeft && shallMoveRight)
            {
                return "Move left and left";
            }
            if (shallMoveLeft)
            {
                return "Move left";
            } 
            if (shallMoveRight)
            {
                return "Move Right";
            }
            
            Debug.LogError("Neither shallMoveLeft nor shallMoveRight is enabled. One must be enabled!");
            return "";
        }
    }
}
