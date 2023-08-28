using System;
using System.Collections.Generic;
using UnityEngine;

public class StoneUpgradeComponent : InteractableBaseComponent
{
    [Serializable]
    public struct Data
    {
        public int woodCost, stoneCost;
        public float miningDuration;
        public int dropAmount;
        public Sprite sprite;
    }

    public delegate void UpgradeMine(float newMiningDuration, int newDropAmount, Sprite sprite);

    public static UpgradeMine OnUpgradeMine;

    protected override void OnResourceDataChanged(DataProvider.CurrentResourceData resourceData)
    {
        var data = DataProvider.Instance.MineData;

        if (upgradeNotificationSprite == null || _dataHandlerComponent.CurrentMineVersion >= data.Count - 1)
        {
            upgradeNotificationSprite.enabled = false;
            return;
        }

        var nextUpgradeData = data[_dataHandlerComponent.CurrentMineVersion + 1];
        var isUpgradable = resourceData.WoodAmount  >= nextUpgradeData.upgradeCost.lumberCost &&
            resourceData.StoneAmount >= nextUpgradeData.upgradeCost.stoneCost;
        upgradeNotificationSprite.enabled = isUpgradable;
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
}