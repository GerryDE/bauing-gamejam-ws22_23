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
            case Interactable.Fence_0_Repair:
            case Interactable.Fence_0_Upgrade:
                version = dataProvider.GetCurrentFenceVersion(0);
                count = dataProvider.FenceData[0].data.Count;
                break;
            case Interactable.Fence_1_Repair:
            case Interactable.Fence_1_Upgrade:
                version = dataProvider.GetCurrentFenceVersion(1);
                count = dataProvider.FenceData[0].data.Count;
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
                count = -1;
                break;
        }

        if (UseNextVersionValue)
        {
            version++;
        }

        string text = "-";
        if (version < count || count == -1)
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
