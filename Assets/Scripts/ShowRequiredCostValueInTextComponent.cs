using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Data;
using AssemblyCSharp.Assets.Scripts;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ShowRequiredLumberCostValueInTextComponent : MonoBehaviour
{
    [SerializeField] private bool UseNextVersionValue;
    [SerializeField] private Currency Currency;
    [SerializeField] private Interactable Interactable;
    private static DataProvider _dataProvider;

    private DataHandlerComponent _dataHandlerComponent;
    private TextMeshProUGUI textComponent;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        _dataHandlerComponent = GameObject.FindWithTag("DataHandler").GetComponent<DataHandlerComponent>();
        _dataProvider = DataProvider.Instance;
    }

    private void OnEnable() {
        textComponent = GetComponent<TextMeshProUGUI>();
        _dataHandlerComponent = GameObject.FindWithTag("DataHandler").GetComponent<DataHandlerComponent>();
        _dataProvider = DataProvider.Instance;

        int version = 0;
        int count = 0;
        switch (Interactable)
        {
            case Interactable.Fence_Repair:
            case Interactable.Fence_Upgrade:
                version = _dataHandlerComponent.CurrentFenceVersion;
                count = _dataProvider.FenceData.Count;
                break;
            case Interactable.Tree_Upgrade:
                version = _dataHandlerComponent.CurrentTreeVersion;
                count = _dataProvider.TreeData.Count;
                break;
            case Interactable.Stone_Upgrade:
                version = _dataHandlerComponent.CurrentMineVersion;
                count = _dataProvider.MineData.Count;
                break;
        }

        if (UseNextVersionValue)
        {
            version++;
        }

        string text = "-";
        if (version < count)
        {
            CostData currentCostData = _dataProvider.GetCostData(Interactable, version);
            switch (Currency)
            {
                case Currency.Lumber:
                    text = currentCostData.lumberCost.ToString();
                    break;
                case Currency.Stone:
                    text = currentCostData.stoneCost.ToString();
                    break;
            }
        }
        textComponent.text = text;
    }
}
