using System;
using UnityEngine;

namespace Data.objective
{
    [CreateAssetMenu(fileName = "Assets/Data/Objective/PraiseStatueObjectiveData", menuName = "Data/PraiseStatueObjectiveData")]
    public class PraiseStatueObjectiveData : ObjectiveData
    {
        public int triggerValueInPercent = 50;
        
        public override string GetObjectiveText()
        {
            return "Your remaining years are getting low!\nHurry, praise the Holy Statue to regain your life!";
        }
    }
}