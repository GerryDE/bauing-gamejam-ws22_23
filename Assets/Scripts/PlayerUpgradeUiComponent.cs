using System;
using TMPro;
using UnityEngine;
using static System.String;

public class PlayerUpgradeUiComponent : PlayerInteractionUiComponent
{
    [SerializeField] private TextMeshPro woodTextComponent;
    [SerializeField] private TextMeshPro stoneTextComponent;
    [SerializeField] private Color enoughResourcesColor;
    [SerializeField] private Color notEnoughResourcesColor;
    private DataHandlerComponent _dataHandlerComponent;
    private int _woodCosts, _stoneCosts;

    protected override void Start()
    {
        base.Start();
        _dataHandlerComponent = GameObject.FindWithTag("DataHandler").GetComponent<DataHandlerComponent>();
        woodTextComponent.text = Empty;
        stoneTextComponent.text = Empty;
    }

    protected override void OnTriggerStay2D(Collider2D other)
    {
        base.OnTriggerStay2D(other);
        
        var layer = LayerMask.LayerToName(other.gameObject.layer);
        if (layer is not ("FenceTrigger" or "Tree" or "Statue" or "Stone")) return;

        if (layer.Equals("FenceTrigger"))
        {
            var data = other.gameObject.GetComponent<FenceUpgradeComponent>().GetData();
            var currentVersion = _dataHandlerComponent.CurrentFenceVersion;
            if (currentVersion >= data.Count - 1)
            {
                textComponent.text = Empty;
                woodTextComponent.text = Empty;
                stoneTextComponent.text = Empty;
                return;
            };
            var nextUpgradeData = data[currentVersion + 1];
            _woodCosts = nextUpgradeData.woodCost;
            _stoneCosts = nextUpgradeData.stoneCost;
        }
        
        if (layer.Equals("Tree"))
        {
            var data = other.gameObject.GetComponent<TreeUpgradeComponent>().GetData();
            var state = other.gameObject.GetComponent<TreeComponent>().GetState();
            var currentVersion = _dataHandlerComponent.CurrentTreeVersion;
            if (state.Equals(TreeComponent.State.Spawning) || currentVersion >= data.Count - 1)
            {
                textComponent.text = Empty;
                woodTextComponent.text = Empty;
                stoneTextComponent.text = Empty;
                return;
            };
            var nextUpgradeData = data[currentVersion + 1];
            _woodCosts = nextUpgradeData.woodCost;
            _stoneCosts = nextUpgradeData.stoneCost;
        }
        
        if (layer.Equals("Statue"))
        {
            var data = other.gameObject.GetComponent<StatueUpgradeComponent>().GetData();
            var currentVersion = _dataHandlerComponent.CurrentStatueVersion;
            if (currentVersion >= data.Count - 1)
            {
                textComponent.text = Empty;
                woodTextComponent.text = Empty;
                stoneTextComponent.text = Empty;
                return;
            };
            var nextUpgradeData = data[currentVersion + 1];
            _woodCosts = nextUpgradeData.woodCost;
            _stoneCosts = nextUpgradeData.stoneCost;
        }
        
        if (layer.Equals("Stone"))
        {
            var data = other.gameObject.GetComponent<StoneUpgradeComponent>().GetData();
            var currentVersion = _dataHandlerComponent.CurrentMineVersion;
            if (currentVersion >= data.Count - 1)
            {
                textComponent.text = Empty;
                woodTextComponent.text = Empty;
                stoneTextComponent.text = Empty;
                return;
            };
            var nextUpgradeData = data[currentVersion + 1];
            _woodCosts = nextUpgradeData.woodCost;
            _stoneCosts = nextUpgradeData.stoneCost;
        }

        woodTextComponent.SetText("Wood: " + _woodCosts);
        stoneTextComponent.SetText("Stone: " + _stoneCosts);

        textComponent.color = _dataHandlerComponent.WoodAmount >= _woodCosts
            ? enoughResourcesColor
            : notEnoughResourcesColor;
        woodTextComponent.color = _dataHandlerComponent.WoodAmount >= _woodCosts
            ? enoughResourcesColor
            : notEnoughResourcesColor;
        stoneTextComponent.color = _dataHandlerComponent.StoneAmount >= _stoneCosts
            ? enoughResourcesColor
            : notEnoughResourcesColor;
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);

        var layer = LayerMask.LayerToName(other.gameObject.layer);
        if (layer is not ("FenceTrigger" or "Tree" or "Statue" or "Stone")) return;

        var leftInteractable = layer switch
        {
            "FenceTrigger" or "Tree" or "Statue" or "Stone" => true,
            _ => false
        };

        if (leftInteractable)
        {
            woodTextComponent.SetText(Empty);
            stoneTextComponent.SetText(Empty);
        }
    }
}