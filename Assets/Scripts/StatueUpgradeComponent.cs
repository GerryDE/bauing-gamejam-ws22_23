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
        DataProvider.OnStatueVersionChanged += OnStatueVersionChanged;
    }

    private void OnStatueVersionChanged(int newVersion)
    {
        if (upgradeNotificationSprite == null) return;
        upgradeNotificationSprite.enabled = IsUpgradeable(newVersion + 1);
    }

    protected override void OnResourceDataChanged(DataProvider.CurrentResourceData resourceData)
    {
        base.OnResourceDataChanged(resourceData);
        if (upgradeNotificationSprite == null) return;
        upgradeNotificationSprite.enabled = IsUpgradeable(DataProvider.Instance.CurrentStatueVersion + 1);
    }

    protected override void OnInteractionButton2Pressed()
    {
        base.OnInteractionButton2Pressed();
        var data = DataProvider.Instance;
        var statueData = data.StatueData;

        _interactionButton2Pressed = false;
        if (!_isCollidingWithPlayer || data.CurrentStatueVersion >= statueData.Count - 1) return;

        var nextUpgradeData = statueData[data.CurrentStatueVersion + 1];
        var resourceData = DataProvider.Instance.ResourceData;
        if (resourceData.WoodAmount < nextUpgradeData.upgradeCost.lumberCost ||
            resourceData.StoneAmount < nextUpgradeData.upgradeCost.stoneCost) return;
        resourceData.WoodAmount -= nextUpgradeData.upgradeCost.lumberCost;
        resourceData.StoneAmount -= nextUpgradeData.upgradeCost.stoneCost;
        OnUpgradeStatue?.Invoke(nextUpgradeData.maxAge, nextUpgradeData.sprite);
        data.CurrentStatueVersion++;
        _dataHandlerComponent.PlayUpgradingAudioClip();
    }

    private bool IsUpgradeable(int nextVersionIndex)
    {
        var data = DataProvider.Instance.StatueData;
        var resourceData = DataProvider.Instance.ResourceData;

        if (nextVersionIndex >= data.Count) return false;

        var nextUpgradeData = data[nextVersionIndex];
        var isUpgradable = resourceData.WoodAmount >= nextUpgradeData.upgradeCost.lumberCost &&
            resourceData.StoneAmount >= nextUpgradeData.upgradeCost.stoneCost;
        return isUpgradable;
    }

    protected override void OnDestroy()
    {
        DataProvider.OnStatueVersionChanged -= OnStatueVersionChanged;
    }
}