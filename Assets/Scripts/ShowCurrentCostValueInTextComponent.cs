using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowCurrentCostValueInTextComponent : MonoBehaviour
{
    public enum Currency
    {
        Lumber,
        Stone
    };

    [SerializeField] private Currency currency;

    private TextMeshProUGUI textComponent;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
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
    }
}
