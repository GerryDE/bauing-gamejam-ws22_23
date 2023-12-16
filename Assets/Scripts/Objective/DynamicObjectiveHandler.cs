using System;
using System.Collections.Generic;
using Data.objective;

namespace Objective
{
    public class DynamicObjectiveHandler
    {
        private List<ObjectiveData> _data;
        private Dictionary<Type, ObjectiveHandler> _handlers;

        public DynamicObjectiveHandler(List<ObjectiveData> data)
        {
            _data = data;
            _handlers = new Dictionary<Type, ObjectiveHandler>();
            foreach (var objectiveData in _data)
            {
                _handlers.Add(typeof(PraiseStatueObjectiveData),
                    new PraiseStatueObjectiveHandler((PraiseStatueObjectiveData) objectiveData));
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