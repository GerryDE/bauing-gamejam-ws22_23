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

    public delegate void RepairFence(int amount);

    public static RepairFence OnRepairFence;

    private FenceController _fenceController;

    protected override void Start()
    {
        base.Start();
        _fenceController = transform.parent.gameObject.GetComponent<FenceController>();
    }

    protected override void OnInteractionButton1Pressed()
    {
        base.OnInteractionButton1Pressed();
        _interactionButton1Pressed = false;

        if (!_isCollidingWithPlayer) return;
        var currentFenceData = DataProvider.Instance.FenceData[_dataHandlerComponent.CurrentFenceVersion];
        var resourceData = DataProvider.Instance.ResourceData;
        if (resourceData.WoodAmount < currentFenceData.repairCost.lumberCost ||
            resourceData.StoneAmount < currentFenceData.repairCost.stoneCost ||
            _fenceController.CurrentHp >= _fenceController.MaxHp) return;
        resourceData.WoodAmount -= currentFenceData.repairCost.lumberCost;
        resourceData.StoneAmount -= currentFenceData.repairCost.stoneCost;
        OnRepairFence?.Invoke(currentFenceData.repairHealAmount);
        _dataHandlerComponent.PlayUpgradingAudioClip();
    }
}