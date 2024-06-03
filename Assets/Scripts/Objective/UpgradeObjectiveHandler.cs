using System;
using Data.objective;

namespace Objective
{
    public class UpgradeObjectiveHandler : ObjectiveHandler
    {
        private UpgradeObjectiveData _data;

        public UpgradeObjectiveHandler(UpgradeObjectiveData data)
        {
            _data = data;

            DataProvider.OnFenceVersionChanged += OnVersionChanged;
            DataProvider.OnMineVersionChanged += OnVersionChanged;
            DataProvider.OnTreeVersionChanged += OnVersionChanged;
            DataProvider.OnStatueVersionChanged += OnVersionChanged;
        }

        private void OnVersionChanged(int newVersion)
        {
            OnObjectiveReached?.Invoke(_data);
            
            DataProvider.OnFenceVersionChanged -= OnVersionChanged;
            DataProvider.OnMineVersionChanged -= OnVersionChanged;
            DataProvider.OnTreeVersionChanged -= OnVersionChanged;
            DataProvider.OnStatueVersionChanged -= OnVersionChanged;
        }
        
        private void OnVersionChanged(int index, int newVersion)
        {
            OnObjectiveReached?.Invoke(_data);
            
            DataProvider.OnFenceVersionChanged -= OnVersionChanged;
            DataProvider.OnMineVersionChanged -= OnVersionChanged;
            DataProvider.OnTreeVersionChanged -= OnVersionChanged;
            DataProvider.OnStatueVersionChanged -= OnVersionChanged;
        }
    }
}