using System;
using System.Collections.Generic;
using Data.objective;

namespace Objective
{
    public class DynamicObjectiveHandler
    {
        private Dictionary<Type, ObjectiveHandler> _handlers;

        public DynamicObjectiveHandler(List<ObjectiveData> data)
        {
            _handlers = new Dictionary<Type, ObjectiveHandler>();
            foreach (var objectiveData in data)
            {
                if (objectiveData.GetType() == typeof(PraiseStatueObjectiveData))
                {
                    _handlers.Add(typeof(PraiseStatueObjectiveData),
                        new PraiseStatueObjectiveHandler((PraiseStatueObjectiveData) objectiveData));
                }
                else if (objectiveData.GetType() == typeof(RepairFenceObjectiveData))
                {
                    _handlers.Add(typeof(RepairFenceObjectiveData),
                        new RepairFenceObjectiveHandler((RepairFenceObjectiveData) objectiveData));
                }
            }

            ObjectiveHandler.OnObjectiveReached += OnObjectiveReached;
        }

        private void OnObjectiveReached(ObjectiveData data)
        {
            if (_handlers.ContainsKey(data.GetType()))
            {
                _handlers.Remove(data.GetType());
            }
        }
    }
}