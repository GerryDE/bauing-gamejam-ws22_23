using System;
using System.Collections.Generic;
using UnityEngine;

public class FenceRepairComponent : InteractableBaseComponent
{
    [Serializable]
    private struct Data
    {
        public int woodCost, stoneCost, healAmount;
    }

    public delegate void RepairFence(int amount);

    public static RepairFence OnRepairFence;

    [SerializeField] private List<Data> data;
    private FenceController _fenceController;

    protected override void Start()
    {
        base.Start();
        _fenceController = transform.parent.gameObject.GetComponent<FenceController>();
    }

    private void Update()
    {
        if (!_interactionButton1Pressed) return;
        _interactionButton1Pressed = false;

        var currentData = data[_dataHandlerComponent.CurrentFenceVersion];
        if (_dataHandlerComponent.WoodAmount >= currentData.woodCost &&
            _dataHandlerComponent.StoneAmount >= currentData.stoneCost &&
            _fenceController.CurrentHp < _fenceController.MaxHp)
        {
            _dataHandlerComponent.WoodAmount -= currentData.woodCost;
            _dataHandlerComponent.StoneAmount -= currentData.stoneCost;
            OnRepairFence?.Invoke(currentData.healAmount);
        }
    }
}