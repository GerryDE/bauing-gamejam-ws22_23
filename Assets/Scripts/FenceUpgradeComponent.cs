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

    protected override void Start()
    {
        base.Start();
        DataHandlerComponent.OnFenceVersionChanged += OnFenceVersionChanged;
    }

    private void OnFenceVersionChanged(int newVersion)
    {
        if (upgradeNotificationSprite == null) return;
        upgradeNotificationSprite.enabled = IsUpgradeable(newVersion);
    }

    protected override void OnResourceDataChanged(DataProvider.CurrentResourceData resourceData)
    {
        base.OnResourceDataChanged(resourceData);
        if (upgradeNotificationSprite == null) return;
        upgradeNotificationSprite.enabled = IsUpgradeable(_dataHandlerComponent.CurrentFenceVersion + 1);
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

    private bool IsUpgradeable(int nextVersionIndex)
    {
        var data = DataProvider.Instance.FenceData;
        var resourceData = DataProvider.Instance.ResourceData;

        if (nextVersionIndex >= data.Count - 1) return false;

        var nextUpgradeData = data[nextVersionIndex];
        var isUpgradable = resourceData.WoodAmount >= nextUpgradeData.upgradeCost.lumberCost &&
            resourceData.StoneAmount >= nextUpgradeData.upgradeCost.stoneCost;
        return isUpgradable;
    }

    protected override void OnDestroy()
    {
        DataHandlerComponent.OnFenceVersionChanged -= OnFenceVersionChanged;
    }
}