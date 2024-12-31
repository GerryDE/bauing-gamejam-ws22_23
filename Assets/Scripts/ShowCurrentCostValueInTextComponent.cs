using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ShowCurrentCostValueInTextComponent : MonoBehaviour
{
    public enum Currency
    {
        Lumber,
        Stone
    };

    [SerializeField] private Currency currency;

    private TextMeshProUGUI textComponent;

    private void OnResourceDataChanged(DataProvider.CurrentResourceData resourceData)
    {
        string text = "0";
        switch (currency)
        {
            case Currency.Lumber:
                text = resourceData.WoodAmount.ToString();
                break;
            case Currency.Stone:
                text = resourceData.StoneAmount.ToString();
                break;
        }
        textComponent.text = text;
    }

    private void OnEnable() {
        textComponent = GetComponent<TextMeshProUGUI>();
        var resourceData = DataProvider.Instance.ResourceData;

        string text = "0";
        switch (currency)
        {
            case Currency.Lumber:
                text = resourceData.WoodAmount.ToString();
                break;
            case Currency.Stone:
                text = resourceData.StoneAmount.ToString();
                break;
        }
        textComponent.text = text;
        DataProvider.OnResourceDataChanged += OnResourceDataChanged;
    }

    private void OnDisable() {
        DataProvider.OnResourceDataChanged -= OnResourceDataChanged;
    }
}
