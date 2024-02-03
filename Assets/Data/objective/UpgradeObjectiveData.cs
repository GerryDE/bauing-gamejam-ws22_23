using UnityEngine;

namespace Data.objective
{
    [CreateAssetMenu(fileName = "Assets/Data/Objective/UpgradeObjectiveData", menuName = "Data/UpgradeObjectiveData")]
    public class UpgradeObjectiveData : ObjectiveData
    {
        public override string GetObjectiveText()
        {
            return "Perform an upgrade. Upgrading the fence ensures more protection.\n"
                   + "Upgrading trees or mine makes you farm more resources.\n"
                   + "Upgrading the statue raises the Player's stats.";
        }
    }
}