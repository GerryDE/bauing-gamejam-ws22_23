using System;
using Data.objective;

namespace Objective
{
    public class PraiseStatueObjectiveHandler : DynamicObjective
    {
        private readonly PraiseStatueObjectiveData _data;
        
        public PraiseStatueObjectiveHandler(PraiseStatueObjectiveData data)
        {
            _data = data;
            DataProvider.OnCurrentRemainingYearsChanged += OnCurrentRemainingYearsChanged;
        }

        private void OnCurrentRemainingYearsChanged(int value)
        {
            if (value <= DataProvider.Instance.PlayerData.MaxRemainingYears / 2)
            {
                EnableObjective(_data);
            }
            else if (value >= DataProvider.Instance.PlayerData.MaxRemainingYears && _objectiveStarted)
            {
                OnObjectiveReached.Invoke(_data);
                DataProvider.OnCurrentRemainingYearsChanged -= OnCurrentRemainingYearsChanged;
            }
        }
    }
}