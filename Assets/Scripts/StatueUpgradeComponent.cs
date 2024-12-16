using System;
using Data.objective;
using Data.upgradeable_objects.statue;
using UnityEngine;
using static Data.upgradeable_objects.statue.StatueData.UpgradeableStat;

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

        var lumberCost = nextStatueData.baseUpgradeCost.lumberCost;
        var lumberCostMultiplier = nextStatueData.lumberCostMultiplier;
        var stoneCost = nextStatueData.baseUpgradeCost.stoneCost;
        var stoneCostMultiplier = nextStatueData.stoneCostMultiplier;

        dataProvider.CurrentStatueData = dataProvider.NextStatueData;

        nextStatueData.upgradeCost.lumberCost = Math.Min(999, (int)(lumberCost *
                                                      Math.Pow(nextStatueVersion,
                                                          lumberCostMultiplier)));
        nextStatueData.upgradeCost.stoneCost = Math.Min(999, (int)(stoneCost *
                                                     Math.Pow(nextStatueVersion,
                                                         stoneCostMultiplier)));
        dataProvider.CurrentStatueData = nextStatueData;

        var playerData = dataProvider.PlayerData;
        var nextStatVersion = nextStatueData.statToUpgrade switch
        {
            MaxHp => playerData.MaxHpLevel,
            Atk => playerData.AttackLevel,
            Def => playerData.DefenseLevel,
            Speed => playerData.SpeedLevel,
            _ => throw new ArgumentOutOfRangeException()
        };
        nextStatVersion++;
        nextStatueData.SetStatValue(nextStatVersion);

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
        if (upgradeNotificationSprite == null || !_upgradeEnabled) return;
        upgradeNotificationSprite.enabled = IsUpgradeable(DataProvider.Instance.CurrentStatueVersion + 1);
    }

    protected override void OnNewObjectiveStarted(ObjectiveData data)
    {
        base.OnNewObjectiveStarted(data);
        if (!_upgradeEnabled) return;
        upgradeNotificationSprite.enabled = IsUpgradeable(DataProvider.Instance.CurrentStatueVersion + 1);
    }

    protected override void OnInteractionButton2Pressed()
    {
        base.OnInteractionButton2Pressed();

         if (!_upgradeEnabled) return; 

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
        switch (currentStatueData.statToUpgrade)
        {
            case MaxHp:
                data.PlayerData.MaxHpLevel++;
                break;
            case Atk:
                data.PlayerData.AttackLevel++;
                break;
            case Def:
                data.PlayerData.DefenseLevel++;
                break;
            case Speed:
                data.PlayerData.SpeedLevel++;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

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