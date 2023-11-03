using System;
using Data.objective;

namespace Objective
{
    public class CollectResourcesObjectiveHandler : ObjectiveHandler
    {
        private CollectResourcesObjectiveData _data;
        private int _initialLumberAmount, _initialStoneAmount;

        public CollectResourcesObjectiveHandler(CollectResourcesObjectiveData data)
        {
            _data = data;
            _initialLumberAmount = DataProvider.Instance.ResourceData.WoodAmount;
            _initialStoneAmount = DataProvider.Instance.ResourceData.StoneAmount;

            DataProvider.OnResourceDataChanged += OnResourceDataChanged;
        }

        private void OnResourceDataChanged(DataProvider.CurrentResourceData data)
        {
            if (data.WoodAmount < _initialLumberAmount + _data.lumberAmount ||
                data.StoneAmount < _initialStoneAmount + _data.stoneAmount) return;
            OnObjectiveReached?.Invoke(_data.GetType());
            DataProvider.OnResourceDataChanged -= OnResourceDataChanged;
        }
    }
}