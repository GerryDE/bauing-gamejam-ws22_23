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

    [SerializeField] private List<Data> data;

    public delegate void UpgradeMine(float newMiningDuration, int newDropAmount, Sprite sprite);

    public static UpgradeMine OnUpgradeMine;

    protected override void OnResourceDataChanged(DataProvider.CurrentResourceData resourceData)
    {
        if (upgradeNotificationSprite == null || _dataHandlerComponent.CurrentMineVersion >= data.Count - 1) return;

        var nextUpgradeData = data[_dataHandlerComponent.CurrentMineVersion + 1];
        var isUpgradable = resourceData.WoodAmount  >= nextUpgradeData.woodCost &&
            resourceData.StoneAmount >= nextUpgradeData.stoneCost;
        upgradeNotificationSprite.enabled = isUpgradable;
    }

    protected override void OnInteractionButton2Pressed()
    {
        base.OnInteractionButton2Pressed();

        _interactionButton2Pressed = false;
        if (!_isCollidingWithPlayer || _dataHandlerComponent.CurrentMineVersion >= data.Count - 1) return;

        var nextUpgradeData = data[_dataHandlerComponent.CurrentMineVersion + 1];
        var resourceData = DataProvider.Instance.ResourceData;
        if (resourceData.WoodAmount < nextUpgradeData.woodCost ||
            resourceData.StoneAmount < nextUpgradeData.stoneCost) return;
        resourceData.WoodAmount -= nextUpgradeData.woodCost;
        resourceData.StoneAmount -= nextUpgradeData.stoneCost;
        OnUpgradeMine?.Invoke(nextUpgradeData.miningDuration, nextUpgradeData.dropAmount, nextUpgradeData.sprite);
        _dataHandlerComponent.PlayUpgradingAudioClip();
    }

    public List<Data> GetData()
    {
        return data;
    }
}