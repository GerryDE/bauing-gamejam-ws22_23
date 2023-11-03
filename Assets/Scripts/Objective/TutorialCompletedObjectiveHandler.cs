using System;
using Data.objective;
using UnityEngine;

namespace Objective
{
    public class TutorialCompletedObjectiveHandler : ObjectiveHandler
    {
        private TutorialCompletedObjectiveData _data;
        private readonly DataHandlerComponent _dataHandlerComponent;

        public TutorialCompletedObjectiveHandler(TutorialCompletedObjectiveData data)
        {
            _data = data;
            _dataHandlerComponent = GameObject.FindWithTag("DataHandler").GetComponent<DataHandlerComponent>();
            _dataHandlerComponent.Wave++;
        }
    }
}