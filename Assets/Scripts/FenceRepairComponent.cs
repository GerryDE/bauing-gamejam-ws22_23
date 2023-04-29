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

    [SerializeField] private List<Data> data;
    private FenceController _fenceController;

    public List<Data> GetData()
    {
        return data;
    }

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
        var currentFenceData = data[_dataHandlerComponent.CurrentFenceVersion];
        var resourceData = DataProvider.Instance.ResourceData;
        if (resourceData.CurrentWoodAmount < currentFenceData.woodCost ||
            resourceData.CurrentStoneAmount< currentFenceData.stoneCost ||
            _fenceController.CurrentHp >= _fenceController.MaxHp) return;
        resourceData.CurrentWoodAmount -= currentFenceData.woodCost;
        resourceData.CurrentStoneAmount -= currentFenceData.stoneCost;
        OnRepairFence?.Invoke(currentFenceData.healAmount);
        _dataHandlerComponent.PlayUpgradingAudioClip();
    }
}
