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

    private DataHandlerComponent _dataHandlerComponent;
    private TextMeshProUGUI textComponent;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        _dataHandlerComponent = GameObject.FindWithTag("DataHandler").GetComponent<DataHandlerComponent>();
    }

    private void OnEnable() {
        textComponent = GetComponent<TextMeshProUGUI>();
        _dataHandlerComponent = GameObject.FindWithTag("DataHandler").GetComponent<DataHandlerComponent>();
        var dataProvider = DataProvider.Instance;

        int version = 0;
        int count = 0;
        switch (Interactable)
        {
            case Interactable.Fence_Repair:
            case Interactable.Fence_Upgrade:
                version = dataProvider.CurrentFenceVersion;
                count = dataProvider.FenceData.Count;
                break;
            case Interactable.Tree_Upgrade:
                version = dataProvider.CurrentTreeVersion;
                count = dataProvider.TreeData.Count;
                break;
            case Interactable.Stone_Upgrade:
                version = dataProvider.CurrentMineVersion;
                count = dataProvider.MineData.Count;
                break;
            case Interactable.Statue_Upgrade:
                version = dataProvider.CurrentStatueVersion;
                count = dataProvider.StatueData.Count;
                break;
        }

        if (UseNextVersionValue)
        {
            version++;
        }

        string text = "-";
        if (version < count)
        {
            CostData currentCostData = DataProvider.Instance.GetCostData(Interactable, version);
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