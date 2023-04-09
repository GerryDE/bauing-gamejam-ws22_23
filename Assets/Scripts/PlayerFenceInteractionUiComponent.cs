using TMPro;
using UnityEngine;
using static System.String;

namespace DefaultNamespace
{
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
            woodTextComponent.text = Empty;
            stoneTextComponent.text = Empty;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            var layer = LayerMask.LayerToName(other.gameObject.layer);
            if (layer != "FenceTrigger")
            {
                textComponent.text = Empty;
                woodTextComponent.text = Empty;
                stoneTextComponent.text = Empty;
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

        private void OnTriggerExit2D(Collider2D other)
        {
            var layer = LayerMask.LayerToName(other.gameObject.layer);
            if (layer is not "FenceTrigger") return;

            woodTextComponent.SetText(Empty);
            stoneTextComponent.SetText(Empty);
        }
    }
}