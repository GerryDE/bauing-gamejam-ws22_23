using System;
using System.Collections.Generic;
using UnityEngine;

public class TreeUpgradeComponent : InteractableBaseComponent
{
    [Serializable]
    public struct Data
    {
        public int woodCost, stoneCost;
    }

    [SerializeField] private List<Data> data;

    public delegate void UpgradeTree();

    public static UpgradeTree OnUpgradeTree;

    protected override void OnResourceDataChanged(DataProvider.CurrentResourceData resourceData)
    {
        if (upgradeNotificationSprite == null || _dataHandlerComponent.CurrentTreeVersion >= data.Count - 1) return;

        var nextUpgradeData = data[_dataHandlerComponent.CurrentTreeVersion + 1];
        var isUpgradable = resourceData.WoodAmount  >= nextUpgradeData.woodCost &&
            resourceData.StoneAmount >= nextUpgradeData.stoneCost;
        upgradeNotificationSprite.enabled = isUpgradable;
    }

    protected override void OnInteractionButton2Pressed()
    {
        base.OnInteractionButton2Pressed();

        _interactionButton2Pressed = false;
        if (!_isCollidingWithPlayer || _dataHandlerComponent.CurrentTreeVersion >= data.Count - 1) return;

        var nextUpgradeData = data[_dataHandlerComponent.CurrentTreeVersion + 1];
        var resourceData = DataProvider.Instance.ResourceData;
        if (resourceData.WoodAmount < nextUpgradeData.woodCost ||
            resourceData.StoneAmount < nextUpgradeData.stoneCost) return;
        resourceData.WoodAmount -= nextUpgradeData.woodCost;
        resourceData.StoneAmount -= nextUpgradeData.stoneCost;
        OnUpgradeTree?.Invoke();
        _dataHandlerComponent.PlayUpgradingAudioClip();
    }

    public List<Data> GetData()
    {
        return data;
    }
}