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
        DataProvider.OnFenceVersionChanged += OnFenceVersionChanged;
    }

    private void OnFenceVersionChanged(int newVersion)
    {
        if (upgradeNotificationSprite == null) return;
        upgradeNotificationSprite.enabled = IsUpgradeable(newVersion + 1);
    }

    protected override void OnResourceDataChanged(DataProvider.CurrentResourceData resourceData)
    {
        base.OnResourceDataChanged(resourceData);
        if (upgradeNotificationSprite == null) return;
        upgradeNotificationSprite.enabled = IsUpgradeable(DataProvider.Instance.CurrentFenceVersion + 1);
    }

    protected override void OnInteractionButton2Pressed()
    {
        base.OnInteractionButton2Pressed();

        var data = DataProvider.Instance;
        var fenceData = DataProvider.Instance.FenceData;

        _interactionButton2Pressed = false;
        if (!_isCollidingWithPlayer || data.CurrentFenceVersion >= fenceData.Count - 1) return;

        var nextUpgradeData = fenceData[data.CurrentFenceVersion + 1];
        var resourceData = DataProvider.Instance.ResourceData;
        if (resourceData.WoodAmount < nextUpgradeData.upgradeCost.lumberCost ||
            resourceData.StoneAmount < nextUpgradeData.upgradeCost.stoneCost) return;
        resourceData.WoodAmount -= nextUpgradeData.upgradeCost.lumberCost;
        resourceData.StoneAmount -= nextUpgradeData.upgradeCost.stoneCost;
        OnUpgradeFence?.Invoke(nextUpgradeData.maxHp, nextUpgradeData.damage, nextUpgradeData.sprite);
        data.CurrentFenceVersion++;
        _dataHandlerComponent.PlayUpgradingAudioClip();
    }

    private bool IsUpgradeable(int nextVersionIndex)
    {
        var data = DataProvider.Instance.FenceData;
        var resourceData = DataProvider.Instance.ResourceData;

        if (nextVersionIndex >= data.Count) return false;

        var nextUpgradeData = data[nextVersionIndex];
        var isUpgradable = resourceData.WoodAmount >= nextUpgradeData.upgradeCost.lumberCost &&
            resourceData.StoneAmount >= nextUpgradeData.upgradeCost.stoneCost;
        return isUpgradable;
    }

    protected override void OnDestroy()
    {
        DataProvider.OnFenceVersionChanged -= OnFenceVersionChanged;
    }
}