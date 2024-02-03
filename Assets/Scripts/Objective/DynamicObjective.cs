using Data.objective;

namespace Objective
{
    public abstract class DynamicObjective : ObjectiveHandler
    {
        public delegate void DynamicObjectiveStarted(ObjectiveData data);

        public static DynamicObjectiveStarted OnDynamicObjectiveStarted;
        
        protected bool _objectiveStarted;
        
        protected void EnableObjective(ObjectiveData data) 
        {
            if (_objectiveStarted) return;
            OnDynamicObjectiveStarted?.Invoke(data);
            _objectiveStarted = true;
        }
    }
}