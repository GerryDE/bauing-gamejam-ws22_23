using System;
using System.Collections.Generic;
using UnityEngine;

public class StatueUpgradeComponent : InteractableBaseComponent
{
    [Serializable]
    public struct Data
    {
        public int woodCost, stoneCost;
        public int newMaxAge;
        public Sprite sprite;
    }

    [SerializeField] private List<Data> data;

    public delegate void UpgradeStatue(int newAgeValue, Sprite sprite);

    public static UpgradeStatue OnUpgradeStatue;

    protected override void OnInteractionButton2Pressed()
    {
        base.OnInteractionButton2Pressed();

        _interactionButton2Pressed = false;
        if (!_isCollidingWithPlayer || _dataHandlerComponent.CurrentStatueVersion >= data.Count - 1) return;

        var nextUpgradeData = data[_dataHandlerComponent.CurrentStatueVersion + 1];
        var resourceData = DataProvider.Instance.ResourceData;
        if (resourceData.CurrentWoodAmount < nextUpgradeData.woodCost ||
            resourceData.CurrentStoneAmount < nextUpgradeData.stoneCost) return;
        resourceData.CurrentWoodAmount -= nextUpgradeData.woodCost;
        resourceData.CurrentStoneAmount -= nextUpgradeData.stoneCost;
        OnUpgradeStatue?.Invoke(nextUpgradeData.newMaxAge, nextUpgradeData.sprite);
        _dataHandlerComponent.CurrentStatueVersion++;
        _dataHandlerComponent.PlayUpgradingAudioClip();
    }

    public List<Data> GetData()
    {
        return data;
    }
}