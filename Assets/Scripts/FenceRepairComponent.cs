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
        var currentData = data[_dataHandlerComponent.CurrentFenceVersion];
        if (_dataHandlerComponent.WoodAmount < currentData.woodCost ||
            _dataHandlerComponent.StoneAmount < currentData.stoneCost ||
            _fenceController.CurrentHp >= _fenceController.MaxHp) return;
        _dataHandlerComponent.WoodAmount -= currentData.woodCost;
        _dataHandlerComponent.StoneAmount -= currentData.stoneCost;
        OnRepairFence?.Invoke(currentData.healAmount);
    }
}