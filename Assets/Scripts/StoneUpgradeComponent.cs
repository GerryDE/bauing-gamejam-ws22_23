using System;
using System.Collections.Generic;
using UnityEngine;

public class StoneUpgradeComponent : InteractableBaseComponent
{
    public delegate void UpgradeMine(float newMiningDuration, int newDropAmount, Sprite sprite);

    public static UpgradeMine OnUpgradeMine;

    protected override void Start()
    {
        base.Start();
        DataHandlerComponent.OnMineVersionChanged += OnMineVersionChanged;
    }

    void OnMineVersionChanged(int newVersion)
    {
        if (upgradeNotificationSprite == null) return;
        upgradeNotificationSprite.enabled = IsUpgradeable(newVersion);
    }


    protected override void OnResourceDataChanged(DataProvider.CurrentResourceData resourceData)
    {
        base.OnResourceDataChanged(resourceData);
        if (upgradeNotificationSprite == null) return;
        upgradeNotificationSprite.enabled = IsUpgradeable(_dataHandlerComponent.CurrentMineVersion + 1);
    }

    protected override void OnInteractionButton2Pressed()
    {
        base.OnInteractionButton2Pressed();

        var data = DataProvider.Instance.MineData;

        _interactionButton2Pressed = false;
        if (!_isCollidingWithPlayer || _dataHandlerComponent.CurrentMineVersion >= data.Count - 1) return;

        var nextUpgradeData = data[_dataHandlerComponent.CurrentMineVersion + 1];
        var resourceData = DataProvider.Instance.ResourceData;
        if (resourceData.WoodAmount < nextUpgradeData.upgradeCost.lumberCost ||
            resourceData.StoneAmount < nextUpgradeData.upgradeCost.stoneCost) return;
        resourceData.WoodAmount -= nextUpgradeData.upgradeCost.lumberCost;
        resourceData.StoneAmount -= nextUpgradeData.upgradeCost.stoneCost;
        OnUpgradeMine?.Invoke(nextUpgradeData.miningDuaration, nextUpgradeData.dropAmount, nextUpgradeData.sprite);
        _dataHandlerComponent.PlayUpgradingAudioClip();
    }

    protected override void OnDestroy()
    {
        DataHandlerComponent.OnMineVersionChanged -= OnMineVersionChanged;
    }

    private bool IsUpgradeable(int nextVersionIndex)
    {
        var data = DataProvider.Instance.MineData;
        var resourceData = DataProvider.Instance.ResourceData;

        if (nextVersionIndex >= data.Count - 1) return false;

        var nextUpgradeData = data[nextVersionIndex];
        var isUpgradable = resourceData.WoodAmount >= nextUpgradeData.upgradeCost.lumberCost &&
            resourceData.StoneAmount >= nextUpgradeData.upgradeCost.stoneCost;
        return isUpgradable;
    }
}