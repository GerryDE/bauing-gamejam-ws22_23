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
            ResetData();
        }

        private static void ResetData()
        {
            DataProvider.Instance.ResourceData.WoodAmount = 0;
            DataProvider.Instance.ResourceData.StoneAmount = 0;
            DataProvider.Instance.CurrentFenceVersion = 0;
            DataProvider.Instance.CurrentTreeVersion = 0;
            DataProvider.Instance.CurrentMineVersion = 0;
            DataProvider.Instance.CurrentStatueVersion = 0;
            DataProvider.Instance.PlayerData.MaxRemainingYears = DataProvider.Instance.StatueData[0].maxAge;
            DataProvider.Instance.PlayerData.CurrentRemainingYears = DataProvider.Instance.PlayerData.MaxRemainingYears;
            
        }
    }
}