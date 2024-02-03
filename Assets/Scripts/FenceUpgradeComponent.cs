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

    private int _index;

    public delegate void UpgradeFence(int index, int newHpValue, int newDamage, Sprite sprite);

    public static UpgradeFence OnUpgradeFence;

    protected override void Start()
    {
        base.Start();

        _index = GetFenceIndex();
        DataProvider.OnFenceVersionChanged += OnFenceVersionChanged;
    }

    private void OnFenceVersionChanged(int index, int newVersion)
    {
        if (index != _index || upgradeNotificationSprite == null) return;
        upgradeNotificationSprite.enabled = IsUpgradeable(newVersion + 1);
    }

    protected override void OnResourceDataChanged(DataProvider.CurrentResourceData resourceData)
    {
        base.OnResourceDataChanged(resourceData);
        if (upgradeNotificationSprite == null) return;
        upgradeNotificationSprite.enabled = IsUpgradeable(DataProvider.Instance.GetCurrentFenceVersion(_index) + 1);
    }

    protected override void OnInteractionButton2Pressed()
    {
        base.OnInteractionButton2Pressed();

        var data = DataProvider.Instance;
        var fenceData = DataProvider.Instance.FenceData[_index];

        _interactionButton2Pressed = false;
        if (!_isCollidingWithPlayer || data.GetCurrentFenceVersion(_index) >= fenceData.data.Count - 1) return;

        var nextUpgradeData = fenceData.data[fenceData.version + 1];
        var resourceData = DataProvider.Instance.ResourceData;
        if (resourceData.WoodAmount < nextUpgradeData.upgradeCost.lumberCost ||
            resourceData.StoneAmount < nextUpgradeData.upgradeCost.stoneCost) return;
        resourceData.WoodAmount -= nextUpgradeData.upgradeCost.lumberCost;
        resourceData.StoneAmount -= nextUpgradeData.upgradeCost.stoneCost;
        OnUpgradeFence?.Invoke(_index, nextUpgradeData.maxHp, nextUpgradeData.damage, nextUpgradeData.sprite);
        data.SetCurrentFenceVersion(_index, data.GetCurrentFenceVersion(_index) + 1);
        _dataHandlerComponent.PlayUpgradingAudioClip();
    }

    private bool IsUpgradeable(int nextVersionIndex)
    {
        var fenceData = DataProvider.Instance.FenceData[_index];
        var resourceData = DataProvider.Instance.ResourceData;

        if (nextVersionIndex >= fenceData.data.Count) return false;

        var nextUpgradeData = fenceData.data[nextVersionIndex];
        var isUpgradable = resourceData.WoodAmount >= nextUpgradeData.upgradeCost.lumberCost &&
            resourceData.StoneAmount >= nextUpgradeData.upgradeCost.stoneCost;
        return isUpgradable;
    }

    protected override void OnDestroy()
    {
        DataProvider.OnFenceVersionChanged -= OnFenceVersionChanged;
    }
    
    private int GetFenceIndex()
    {
        return transform.parent.TryGetComponent<FenceController>(out var fenceController) ? fenceController.fenceIndex : 0;
    }
}