using System;
using System.Collections.Generic;
using UnityEngine;

public class FenceUpgradeComponent : InteractableBaseComponent
{
    [Serializable]
    private struct Data
    {
        public int woodCost, stoneCost, newHp;
        public Sprite sprite;
    }

    [SerializeField] private List<Data> data;

    public delegate void UpgradeFence(int newHpValue, Sprite sprite);

    public static UpgradeFence OnUpgradeFence;

    private void Update()
    {
        if (!_interactionButton2Pressed || _dataHandlerComponent.CurrentFenceVersion >= data.Count)
        {
            _interactionButton2Pressed = false;
            return;
        }

        var nextUpgradeData = data[_dataHandlerComponent.CurrentFenceVersion + 1];
        if (_dataHandlerComponent.WoodAmount >= nextUpgradeData.woodCost &&
            _dataHandlerComponent.StoneAmount >= nextUpgradeData.stoneCost)
        {
            _dataHandlerComponent.WoodAmount -= nextUpgradeData.woodCost;
            _dataHandlerComponent.StoneAmount -= nextUpgradeData.stoneCost;

            OnUpgradeFence?.Invoke(nextUpgradeData.newHp, nextUpgradeData.sprite);
            _dataHandlerComponent.CurrentFenceVersion++;
        }

        _interactionButton2Pressed = false;
    }
}