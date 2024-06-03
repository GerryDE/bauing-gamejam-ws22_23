using System;
using Data.objective;

namespace Objective
{
    public class RepairFenceObjectiveHandler : DynamicObjective
    {
        private readonly RepairFenceObjectiveData _data;
        
        public RepairFenceObjectiveHandler(RepairFenceObjectiveData data)
        {
            _data = data;
            FenceController.OnCurrentHpChanged += OnCurrentFenceHpChanged;
            FenceRepairComponent.OnRepairFence += OnRepairFence;
        }

        private void OnCurrentFenceHpChanged(int value, int maxHp)
        {
            if (value <= maxHp * _data.triggerValueInPercent / 100)
            {
                EnableObjective(_data);
                FenceController.OnCurrentHpChanged -= OnCurrentFenceHpChanged;
            }
        }
        
        private void OnRepairFence(int index, int amount)
        {
            if (!_objectiveStarted) return;
            OnObjectiveReached.Invoke(_data);
            FenceRepairComponent.OnRepairFence -= OnRepairFence;
        }
    }
}