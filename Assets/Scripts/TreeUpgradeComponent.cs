using System;
using System.Collections.Generic;
using UnityEngine;

public class TreeUpgradeComponent : InteractableBaseComponent
{
    public delegate void UpgradeTree();

    public static UpgradeTree OnUpgradeTree;

    protected override void Start()
    {
        base.Start();
        DataProvider.OnTreeVersionChanged += OnTreeVersionChanged;
    }

    private void OnTreeVersionChanged(int newVersion)
    {
        if (upgradeNotificationSprite == null) return;
        upgradeNotificationSprite.enabled = IsUpgradeable(newVersion + 1);
    }

    protected override void OnResourceDataChanged(DataProvider.CurrentResourceData resourceData)
    {
        base.OnResourceDataChanged(resourceData);
        if (upgradeNotificationSprite == null) return;
        upgradeNotificationSprite.enabled = IsUpgradeable(DataProvider.Instance.CurrentTreeVersion + 1);
    }

    protected override void OnInteractionButton2Pressed()
    {
        base.OnInteractionButton2Pressed();

        _interactionButton2Pressed = false;
        var data = DataProvider.Instance;
        var treeData = data.TreeData;
        if (!_isCollidingWithPlayer || data.CurrentTreeVersion >= treeData.Count - 1) return;

        var nextUpgradeData = treeData[data.CurrentTreeVersion + 1];
        var resourceData = data.ResourceData;
        if (resourceData.WoodAmount < nextUpgradeData.upgradeCost.lumberCost ||
            resourceData.StoneAmount < nextUpgradeData.upgradeCost.stoneCost) return;
        resourceData.WoodAmount -= nextUpgradeData.upgradeCost.lumberCost;
        resourceData.StoneAmount -= nextUpgradeData.upgradeCost.stoneCost;
        OnUpgradeTree?.Invoke();
        _dataHandlerComponent.PlayUpgradingAudioClip();
    }

    private bool IsUpgradeable(int nextVersionIndex)
    {
        var data = DataProvider.Instance;
        var treeData = data.TreeData;
        var resourceData = data.ResourceData;

        if (nextVersionIndex >= treeData.Count) return false;

        var nextUpgradeData = treeData[nextVersionIndex];
        var isUpgradable = resourceData.WoodAmount >= nextUpgradeData.upgradeCost.lumberCost &&
            resourceData.StoneAmount >= nextUpgradeData.upgradeCost.stoneCost;
        return isUpgradable;
    }

    protected override void OnDestroy()
    {
        DataProvider.OnTreeVersionChanged -= OnTreeVersionChanged;
    }
}