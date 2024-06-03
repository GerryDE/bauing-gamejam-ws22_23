using System;
using System.Collections.Generic;
using UnityEngine;

public class FenceRepairComponent : InteractableBaseComponent
{
    [Serializable]
    public struct Data
    {
        public int woodCost, stoneCost, healAmount;
    }

    public delegate void RepairFence(int fenceIndex, int amount);

    public static RepairFence OnRepairFence;

    private FenceController _fenceController;
    private int _index;

    protected override void Start()
    {
        base.Start();
        _fenceController = transform.parent.gameObject.GetComponent<FenceController>();
        _index = _fenceController.fenceIndex;
    }

    protected override void OnInteractionButton1Pressed()
    {
        base.OnInteractionButton1Pressed();
        _interactionButton1Pressed = false;

        if (!_isCollidingWithPlayer) return;
        var data = DataProvider.Instance;
        var currentFenceData = data.FenceData[_index].data[data.FenceData[_index].version];
        var resourceData = data.ResourceData;
        if (resourceData.WoodAmount < currentFenceData.repairCost.lumberCost ||
            resourceData.StoneAmount < currentFenceData.repairCost.stoneCost ||
            _fenceController.CurrentHp >= _fenceController.MaxHp) return;
        resourceData.WoodAmount -= currentFenceData.repairCost.lumberCost;
        resourceData.StoneAmount -= currentFenceData.repairCost.stoneCost;
        OnRepairFence?.Invoke(_index, currentFenceData.repairHealAmount);
        _dataHandlerComponent.PlayUpgradingAudioClip();
    }
}