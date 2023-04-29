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

    protected override void OnInteractionButton2Pressed()
    {
        base.OnInteractionButton2Pressed();

        _interactionButton2Pressed = false;
        if (!_isCollidingWithPlayer || _dataHandlerComponent.CurrentTreeVersion >= data.Count - 1) return;

        var nextUpgradeData = data[_dataHandlerComponent.CurrentTreeVersion + 1];
        var resourceData = DataProvider.Instance.ResourceData;
        if (resourceData.CurrentWoodAmount < nextUpgradeData.woodCost ||
            resourceData.CurrentStoneAmount < nextUpgradeData.stoneCost) return;
        resourceData.CurrentWoodAmount -= nextUpgradeData.woodCost;
        resourceData.CurrentStoneAmount -= nextUpgradeData.stoneCost;
        OnUpgradeTree?.Invoke();
        _dataHandlerComponent.PlayUpgradingAudioClip();
    }

    public List<Data> GetData()
    {
        return data;
    }
}
