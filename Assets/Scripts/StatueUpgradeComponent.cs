using System;
using Data;
using Data.upgradeable_objects.statue;
using UnityEngine;

public class StatueUpgradeComponent : InteractableBaseComponent
{
    public delegate void UpgradeStatue(StatueData.UpgradeableStat stat, float value);

    public static UpgradeStatue OnUpgradeStatue;

    protected override void Start()
    {
        base.Start();
        DataProvider.OnStatueVersionChanged += OnStatueVersionChanged;
        
        GenerateNextStatueData();
    }

    private static void GenerateNextStatueData()
    {
        var dataProvider = DataProvider.Instance;
        var currentStatueData = dataProvider.CurrentStatueData;
        var nextStatueVersion = dataProvider.CurrentStatueVersion + 1;

        var nextStatueData = currentStatueData.Copy();
        nextStatueData.upgradeCost.lumberCost = (int)(currentStatueData.baseUpgradeCost.lumberCost *
                                                    Math.Pow(nextStatueVersion,
                                                        currentStatueData.lumberCostMultiplier));
        nextStatueData.upgradeCost.stoneCost = (int)(currentStatueData.baseUpgradeCost.stoneCost *
                                                   Math.Pow(nextStatueVersion,
                                                       currentStatueData.stoneCostMultiplier));
        nextStatueData.SetStatValue(nextStatueVersion);
        
        DataProvider.Instance.NextStatueData = nextStatueData;
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
        var currentStatueData = data.CurrentStatueData;

        _interactionButton2Pressed = false;
        if (!_isCollidingWithPlayer) return;

        var nextStatueData = data.NextStatueData;
        var resourceData = data.ResourceData;
        if (resourceData.WoodAmount < nextStatueData.upgradeCost.lumberCost ||
            resourceData.StoneAmount < nextStatueData.upgradeCost.stoneCost) return;
        resourceData.WoodAmount -= nextStatueData.upgradeCost.lumberCost;
        resourceData.StoneAmount -= nextStatueData.upgradeCost.stoneCost;
        OnUpgradeStatue?.Invoke(nextStatueData.statToUpgrade, nextStatueData.statValue);
        data.CurrentStatueVersion++;
        GenerateNextStatueData();
        _dataHandlerComponent.PlayUpgradingAudioClip();
    }

    private static bool IsUpgradeable(int nextVersionIndex)
    {
        var data = DataProvider.Instance.NextStatueData;
        var resourceData = DataProvider.Instance.ResourceData;
        
        var isUpgradable = resourceData.WoodAmount >= data.upgradeCost.lumberCost &&
                           resourceData.StoneAmount >= data.upgradeCost.stoneCost;
        return isUpgradable;
    }

    protected override void OnDestroy()
    {
        DataProvider.OnStatueVersionChanged -= OnStatueVersionChanged;
    }
}