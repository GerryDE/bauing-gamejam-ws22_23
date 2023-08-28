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

    public delegate void UpgradeFence(int newHpValue, int newDamage, Sprite sprite);

    public static UpgradeFence OnUpgradeFence;

    protected override void OnResourceDataChanged(DataProvider.CurrentResourceData resourceData)
    {
        var fenceData = DataProvider.Instance.FenceData;

        if (upgradeNotificationSprite == null || _dataHandlerComponent.CurrentFenceVersion >= fenceData.Count - 1) return;

        var nextUpgradeData = fenceData[_dataHandlerComponent.CurrentFenceVersion + 1];
        var isUpgradable = resourceData.WoodAmount  >= nextUpgradeData.upgradeCost.lumberCost &&
            resourceData.StoneAmount >= nextUpgradeData.upgradeCost.stoneCost;
        upgradeNotificationSprite.enabled = isUpgradable;
    }

    protected override void OnInteractionButton2Pressed()
    {
        base.OnInteractionButton2Pressed();

        var fenceData = DataProvider.Instance.FenceData;

        _interactionButton2Pressed = false;
        if (!_isCollidingWithPlayer || _dataHandlerComponent.CurrentFenceVersion >= fenceData.Count - 1) return;

        var nextUpgradeData = fenceData[_dataHandlerComponent.CurrentFenceVersion + 1];
        var resourceData = DataProvider.Instance.ResourceData;
        if (resourceData.WoodAmount < nextUpgradeData.upgradeCost.lumberCost ||
            resourceData.StoneAmount < nextUpgradeData.upgradeCost.stoneCost) return;
        resourceData.WoodAmount -= nextUpgradeData.upgradeCost.lumberCost;
        resourceData.StoneAmount -= nextUpgradeData.upgradeCost.stoneCost;
        OnUpgradeFence?.Invoke(nextUpgradeData.maxHp, nextUpgradeData.damage, nextUpgradeData.sprite);
        _dataHandlerComponent.CurrentFenceVersion++;
        _dataHandlerComponent.PlayUpgradingAudioClip();
    }
}