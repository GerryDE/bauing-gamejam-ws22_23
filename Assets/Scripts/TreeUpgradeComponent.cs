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

    protected override void Start()
    {
        base.Start();
        DataHandlerComponent.OnTreeVersionChanged += OnTreeVersionChanged;
    }

    private void OnTreeVersionChanged(int newVersion)
    {
        if (upgradeNotificationSprite == null) return;
        upgradeNotificationSprite.enabled = IsUpgradeable(newVersion);
    }

    protected override void OnResourceDataChanged(DataProvider.CurrentResourceData resourceData)
    {
        base.OnResourceDataChanged(resourceData);
        if (upgradeNotificationSprite == null) return;
        upgradeNotificationSprite.enabled = IsUpgradeable(_dataHandlerComponent.CurrentTreeVersion + 1);
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

    protected override void OnDestroy()
    {
        DataHandlerComponent.OnTreeVersionChanged -= OnTreeVersionChanged;
    }

    private bool IsUpgradeable(int nextVersionIndex)
    {
        var treeData = DataProvider.Instance.TreeData;
        var resourceData = DataProvider.Instance.ResourceData;

        if (nextVersionIndex >= treeData.Count - 1) return false;

        var nextUpgradeData = treeData[nextVersionIndex];
        var isUpgradable = resourceData.WoodAmount >= nextUpgradeData.upgradeCost.lumberCost &&
            resourceData.StoneAmount >= nextUpgradeData.upgradeCost.stoneCost;
        return isUpgradable;
    }
}