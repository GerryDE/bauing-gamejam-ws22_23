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

    // Start is called before the first frame update
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

        int version = _dataHandlerComponent.CurrentFenceVersion;
        if (UseNextVersionValue)
        {
            version++;
        }

        List<FenceData> fenceData = _dataProvider.FenceData;

        string text = "-";
        if (version < fenceData.Count)
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
