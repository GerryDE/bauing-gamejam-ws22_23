using UnityEngine;

namespace Data.objective
{
    public class ObjectiveData : ScriptableObject
    {
        public virtual string GetObjectiveText()
        {
            return "<Default objective text>";
        }
    }
}
