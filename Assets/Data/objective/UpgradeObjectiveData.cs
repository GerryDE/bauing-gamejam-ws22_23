using UnityEngine;

namespace Data.objective
{
    [CreateAssetMenu(fileName = "Assets/Data/Objective/UpgradeObjectiveData", menuName = "Data/UpgradeObjectiveData")]
    public class UpgradeObjectiveData : ObjectiveData
    {
        public override string GetObjectiveText()
        {
            return "Perform an upgrade";
        }
    }
}