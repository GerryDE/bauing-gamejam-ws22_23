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

    protected override void OnResourceDataChanged(DataProvider.CurrentResourceData resourceData)
    {
        if (upgradeNotificationSprite == null || _dataHandlerComponent.CurrentFenceVersion >= data.Count - 1) return;

        var nextUpgradeData = data[_dataHandlerComponent.CurrentFenceVersion + 1];
        var isUpgradable = resourceData.WoodAmount  >= nextUpgradeData.woodCost &&
            resourceData.StoneAmount >= nextUpgradeData.stoneCost;
        upgradeNotificationSprite.enabled = isUpgradable;
    }

    protected override void OnInteractionButton2Pressed()
    {
        base.OnInteractionButton2Pressed();

        _interactionButton2Pressed = false;
        if (!_isCollidingWithPlayer || _dataHandlerComponent.CurrentFenceVersion >= data.Count - 1) return;

        var nextUpgradeData = data[_dataHandlerComponent.CurrentFenceVersion + 1];
        var resourceData = DataProvider.Instance.ResourceData;
        if (resourceData.WoodAmount < nextUpgradeData.woodCost ||
            resourceData.StoneAmount < nextUpgradeData.stoneCost) return;
        resourceData.WoodAmount -= nextUpgradeData.woodCost;
        resourceData.StoneAmount -= nextUpgradeData.stoneCost;
        OnUpgradeFence?.Invoke(nextUpgradeData.newHp, nextUpgradeData.damage, nextUpgradeData.sprite);
        _dataHandlerComponent.CurrentFenceVersion++;
        _dataHandlerComponent.PlayUpgradingAudioClip();
    }
}