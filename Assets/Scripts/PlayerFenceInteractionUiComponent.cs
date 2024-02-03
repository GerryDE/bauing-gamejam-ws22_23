// using System;
// using TMPro;
// using UnityEngine;
//
// public class PlayerFenceInteractionUiComponent : MonoBehaviour
// {
//     [SerializeField] protected string buttonText = "W";
//     [SerializeField] private string fenceText;
//     [SerializeField] private TextMeshPro textComponent;
//     [SerializeField] private TextMeshPro woodTextComponent;
//     [SerializeField] private TextMeshPro stoneTextComponent;
//     [SerializeField] private Color enoughResourcesColor;
//     [SerializeField] private Color notEnoughResourcesColor;
//
//     private DataHandlerComponent _dataHandlerComponent;
//     private int _woodCosts, _stoneCosts;
//
//     private void Start()
//     {
//         _dataHandlerComponent = GameObject.FindWithTag("DataHandler").GetComponent<DataHandlerComponent>();
//         woodTextComponent.text = String.Empty;
//         stoneTextComponent.text = String.Empty;
//     }
//
//     private void OnTriggerStay2D(Collider2D other)
//     {
//         var layer = LayerMask.LayerToName(other.gameObject.layer);
//         if (layer != "FenceTrigger")
//         {
//             textComponent.text = String.Empty;
//             woodTextComponent.text = String.Empty;
//             stoneTextComponent.text = String.Empty;
//             return;
//         }
//
//         textComponent.SetText("[" + buttonText + "] " + fenceText);
//
//         var data = DataProvider.Instance;
//         var fenceData = data.FenceData;
//         var currentVersion = data.CurrentFenceVersion;
//         var currentData = fenceData[currentVersion];
//         _woodCosts = currentData.repairCost.lumberCost;
//         _stoneCosts = currentData.repairCost.stoneCost;
//
//         woodTextComponent.SetText("Wood: " + _woodCosts);
//         stoneTextComponent.SetText("Stone: " + _stoneCosts);
//
//         var resourceData = data.ResourceData;
//         textComponent.color = resourceData.WoodAmount >= _woodCosts
//             ? enoughResourcesColor
//             : notEnoughResourcesColor;
//         woodTextComponent.color = resourceData.WoodAmount >= _woodCosts
//             ? enoughResourcesColor
//             : notEnoughResourcesColor;
//         stoneTextComponent.color = resourceData.StoneAmount >= _stoneCosts
//             ? enoughResourcesColor
//             : notEnoughResourcesColor;
//     }
//
//     private void OnTriggerExit2D(Collider2D other)
//     {
//         var layer = LayerMask.LayerToName(other.gameObject.layer);
//         if (layer is not "FenceTrigger") return;
//
//         woodTextComponent.SetText(String.Empty);
//         stoneTextComponent.SetText(String.Empty);
//     }
// }