using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StatueUpgradeComponent : InteractableBaseComponent
{
    [Serializable]
    private struct Data
    {
        public int woodCost, stoneCost;
        public int newMaxAge;
        public Sprite sprite;
    }

    [SerializeField] private List<Data> data;

    public delegate void UpgradeStatue(int newAgeValue, Sprite sprite);

    public static UpgradeStatue OnUpgradeStatue;

    private void Update()
    {
        if (!_interactionButton2Pressed || _dataHandlerComponent.CurrentStatueVersion >= data.Count)
        {
            _interactionButton2Pressed = false;
            return;
        }

        var nextUpgradeData = data[_dataHandlerComponent.CurrentStatueVersion + 1];
        if (_dataHandlerComponent.WoodAmount >= nextUpgradeData.woodCost &&
            _dataHandlerComponent.StoneAmount >= nextUpgradeData.stoneCost)
        {
            _dataHandlerComponent.WoodAmount -= nextUpgradeData.woodCost;
            _dataHandlerComponent.StoneAmount -= nextUpgradeData.stoneCost;

            OnUpgradeStatue?.Invoke(nextUpgradeData.newMaxAge, nextUpgradeData.sprite);
            _dataHandlerComponent.CurrentStatueVersion++;
        }

        _interactionButton2Pressed = false;
    }
}
