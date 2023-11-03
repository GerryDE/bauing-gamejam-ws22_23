using UnityEngine;

namespace Data.objective
{
    [CreateAssetMenu(fileName = "Assets/Data/Objective/TutorialCompletedObjectiveData", menuName = "Data/TutorialCompletedObjectiveData")]
    public class TutorialCompletedObjectiveData : ObjectiveData
    {
        public override string GetObjectiveText()
        {
            return "Tutorial completed! Now, protect the Goddess' Statue and defeat the Army of Darkness!";
        }
    }
}