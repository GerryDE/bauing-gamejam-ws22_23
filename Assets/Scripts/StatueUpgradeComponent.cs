using System;
using System.Collections.Generic;
using UnityEngine;

public class StatueUpgradeComponent : InteractableBaseComponent
{
    public delegate void UpgradeStatue(int newAgeValue, Sprite sprite);

    public static UpgradeStatue OnUpgradeStatue;

    protected override void Start()
    {
        base.Start();
        DataHandlerComponent.OnStatueVersionChanged += OnStatueVersionChanged;
    }

    private void OnStatueVersionChanged(int newVersion)
    {
        if (upgradeNotificationSprite == null) return;
        upgradeNotificationSprite.enabled = IsUpgradeable(newVersion);
    }

    protected override void OnResourceDataChanged(DataProvider.CurrentResourceData resourceData)
    {
        base.OnResourceDataChanged(resourceData);
        if (upgradeNotificationSprite == null) return;
        upgradeNotificationSprite.enabled = IsUpgradeable(_dataHandlerComponent.CurrentStatueVersion + 1);
    }

    protected override void OnInteractionButton2Pressed()
    {
        base.OnInteractionButton2Pressed();
        var data = DataProvider.Instance.StatueData;

        _interactionButton2Pressed = false;
        if (!_isCollidingWithPlayer || _dataHandlerComponent.CurrentStatueVersion >= data.Count - 1) return;

        var nextUpgradeData = data[_dataHandlerComponent.CurrentStatueVersion + 1];
        var resourceData = DataProvider.Instance.ResourceData;
        if (resourceData.WoodAmount < nextUpgradeData.upgradeCost.lumberCost ||
            resourceData.StoneAmount < nextUpgradeData.upgradeCost.stoneCost) return;
        resourceData.WoodAmount -= nextUpgradeData.upgradeCost.lumberCost;
        resourceData.StoneAmount -= nextUpgradeData.upgradeCost.stoneCost;
        OnUpgradeStatue?.Invoke(nextUpgradeData.maxAge, nextUpgradeData.sprite);
        _dataHandlerComponent.CurrentStatueVersion++;
        _dataHandlerComponent.PlayUpgradingAudioClip();
    }

    protected override void OnDestroy()
    {
        DataHandlerComponent.OnStatueVersionChanged -= OnStatueVersionChanged;
    }

    private bool IsUpgradeable(int nextVersionIndex)
    {
        var data = DataProvider.Instance.StatueData;
        var resourceData = DataProvider.Instance.ResourceData;

        if (nextVersionIndex >= data.Count - 1) return false;

        var nextUpgradeData = data[nextVersionIndex];
        var isUpgradable = resourceData.WoodAmount >= nextUpgradeData.upgradeCost.lumberCost &&
            resourceData.StoneAmount >= nextUpgradeData.upgradeCost.stoneCost;
        return isUpgradable;
    }
}