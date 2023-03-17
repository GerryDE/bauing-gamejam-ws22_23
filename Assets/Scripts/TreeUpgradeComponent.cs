using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
        if (_dataHandlerComponent.WoodAmount < nextUpgradeData.woodCost ||
            _dataHandlerComponent.StoneAmount < nextUpgradeData.stoneCost) return;
        _dataHandlerComponent.WoodAmount -= nextUpgradeData.woodCost;
        _dataHandlerComponent.StoneAmount -= nextUpgradeData.stoneCost;
        OnUpgradeTree?.Invoke();
        _dataHandlerComponent.PlayUpgradingAudioClip();
    }

    public List<Data> GetData()
    {
        return data;
    }
}
