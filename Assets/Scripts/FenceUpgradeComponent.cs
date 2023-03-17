using System;
using System.Collections.Generic;
using UnityEngine;

public class FenceUpgradeComponent : InteractableBaseComponent
{
    [Serializable]
    public struct Data
    {
        public int woodCost, stoneCost, newHp, damage;
        public Sprite sprite;
    }

    [SerializeField] private List<Data> data;

    public delegate void UpgradeFence(int newHpValue, int newDamage, Sprite sprite);

    public static UpgradeFence OnUpgradeFence;

    public List<Data> GetData()
    {
        return data;
    }

    protected override void OnInteractionButton2Pressed()
    {
        base.OnInteractionButton2Pressed();
        
        _interactionButton2Pressed = false;
        if (!_isCollidingWithPlayer || _dataHandlerComponent.CurrentFenceVersion >= data.Count - 1) return;
        
        var nextUpgradeData = data[_dataHandlerComponent.CurrentFenceVersion + 1];
        if (_dataHandlerComponent.WoodAmount < nextUpgradeData.woodCost ||
            _dataHandlerComponent.StoneAmount < nextUpgradeData.stoneCost) return;
        _dataHandlerComponent.WoodAmount -= nextUpgradeData.woodCost;
        _dataHandlerComponent.StoneAmount -= nextUpgradeData.stoneCost;
        OnUpgradeFence?.Invoke(nextUpgradeData.newHp, nextUpgradeData.damage, nextUpgradeData.sprite);
        _dataHandlerComponent.CurrentFenceVersion++;
    }
}