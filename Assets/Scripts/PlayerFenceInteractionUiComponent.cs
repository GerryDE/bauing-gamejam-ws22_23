using System;
using TMPro;
using UnityEngine;

public class PlayerFenceInteractionUiComponent : MonoBehaviour
{
    [SerializeField] protected string buttonText = "W";
    [SerializeField] private string fenceText;
    [SerializeField] private TextMeshPro textComponent;
    [SerializeField] private TextMeshPro woodTextComponent;
    [SerializeField] private TextMeshPro stoneTextComponent;
    [SerializeField] private Color enoughResourcesColor;
    [SerializeField] private Color notEnoughResourcesColor;

    private DataHandlerComponent _dataHandlerComponent;
    private int _woodCosts, _stoneCosts;

    private void Start()
    {
        _dataHandlerComponent = GameObject.FindWithTag("DataHandler").GetComponent<DataHandlerComponent>();
        woodTextComponent.text = String.Empty;
        stoneTextComponent.text = String.Empty;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var layer = LayerMask.LayerToName(other.gameObject.layer);
        if (layer != "FenceTrigger")
        {
            textComponent.text = String.Empty;
            woodTextComponent.text = String.Empty;
            stoneTextComponent.text = String.Empty;
            return;
        }

        textComponent.SetText("[" + buttonText + "] " + fenceText);

        var data = other.gameObject.GetComponent<FenceRepairComponent>().GetData();
        var currentVersion = _dataHandlerComponent.CurrentFenceVersion;
        var currentData = data[currentVersion];
        _woodCosts = currentData.woodCost;
        _stoneCosts = currentData.stoneCost;

        woodTextComponent.SetText("Wood: " + _woodCosts);
        stoneTextComponent.SetText("Stone: " + _stoneCosts);

        var resourceData = DataProvider.Instance.ResourceData;
        textComponent.color = resourceData.CurrentWoodAmount >= _woodCosts
            ? enoughResourcesColor
            : notEnoughResourcesColor;
        woodTextComponent.color = resourceData.CurrentWoodAmount >= _woodCosts
            ? enoughResourcesColor
            : notEnoughResourcesColor;
        stoneTextComponent.color = resourceData.CurrentStoneAmount >= _stoneCosts
            ? enoughResourcesColor
            : notEnoughResourcesColor;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var layer = LayerMask.LayerToName(other.gameObject.layer);
        if (layer is not "FenceTrigger") return;

        woodTextComponent.SetText(String.Empty);
        stoneTextComponent.SetText(String.Empty);
    }
}
