// using TMPro;
// using UnityEngine;
// using static System.String;
//
// public class PlayerUpgradeUiComponent : PlayerInteractionUiComponent
// {
//     [SerializeField] private TextMeshPro woodTextComponent;
//     [SerializeField] private TextMeshPro stoneTextComponent;
//     [SerializeField] private Color enoughResourcesColor;
//     [SerializeField] private Color notEnoughResourcesColor;
//     private DataHandlerComponent _dataHandlerComponent;
//     private int _woodCosts, _stoneCosts;
//
//     protected override void Start()
//     {
//         base.Start();
//         _dataHandlerComponent = GameObject.FindWithTag("DataHandler").GetComponent<DataHandlerComponent>();
//         woodTextComponent.text = Empty;
//         stoneTextComponent.text = Empty;
//     }
//
//     protected override void OnTriggerStay2D(Collider2D other)
//     {
//         base.OnTriggerStay2D(other);
//
//         var layer = LayerMask.LayerToName(other.gameObject.layer);
//         if (layer is not ("FenceTrigger" or "Tree" or "Statue" or "Stone")) return;
//
//         var data = DataProvider.Instance;
//         if (layer.Equals("FenceTrigger"))
//         {
//             var fenceData = data.FenceData;
//             var currentVersion = data.CurrentFenceVersion;
//             if (currentVersion >= fenceData.Count - 1)
//             {
//                 textComponent.text = Empty;
//                 woodTextComponent.text = Empty;
//                 stoneTextComponent.text = Empty;
//                 return;
//             }
//
//             var nextUpgradeData = fenceData[currentVersion + 1];
//             _woodCosts = nextUpgradeData.upgradeCost.lumberCost;
//             _stoneCosts = nextUpgradeData.upgradeCost.stoneCost;
//         }
//
//         if (layer.Equals("Tree"))
//         {
//             var treeData = DataProvider.Instance.TreeData;
//             var state = other.gameObject.GetComponent<TreeComponent>().GetState();
//             var currentVersion = data.CurrentTreeVersion;
//             if (state.Equals(TreeComponent.State.Spawning) || currentVersion >= treeData.Count - 1)
//             {
//                 textComponent.text = Empty;
//                 woodTextComponent.text = Empty;
//                 stoneTextComponent.text = Empty;
//                 return;
//             }
//
//             var nextUpgradeData = treeData[currentVersion + 1];
//             _woodCosts = nextUpgradeData.upgradeCost.lumberCost;
//             _stoneCosts = nextUpgradeData.upgradeCost.stoneCost;
//         }
//
//         if (layer.Equals("Statue"))
//         {
//             var statueData = DataProvider.Instance.StatueData;
//             var currentVersion = data.CurrentStatueVersion;
//             if (currentVersion >= statueData.Count - 1)
//             {
//                 textComponent.text = Empty;
//                 woodTextComponent.text = Empty;
//                 stoneTextComponent.text = Empty;
//                 return;
//             }
//
//             var nextUpgradeData = statueData[currentVersion + 1];
//             _woodCosts = nextUpgradeData.upgradeCost.lumberCost;
//             _stoneCosts = nextUpgradeData.upgradeCost.stoneCost;
//         }
//
//         if (layer.Equals("Stone"))
//         {
//             var mineData = DataProvider.Instance.MineData;
//             var currentVersion = data.CurrentMineVersion;
//             if (currentVersion >= mineData.Count - 1)
//             {
//                 textComponent.text = Empty;
//                 woodTextComponent.text = Empty;
//                 stoneTextComponent.text = Empty;
//                 return;
//             }
//
//             var nextUpgradeData = mineData[currentVersion + 1];
//             _woodCosts = nextUpgradeData.upgradeCost.lumberCost;
//             _stoneCosts = nextUpgradeData.upgradeCost.stoneCost;
//         }
//
//         woodTextComponent.SetText("Wood: " + _woodCosts);
//         stoneTextComponent.SetText("Stone: " + _stoneCosts);
//
//         var resourceData = DataProvider.Instance.ResourceData;
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
//     protected override void OnTriggerExit2D(Collider2D other)
//     {
//         base.OnTriggerExit2D(other);
//
//         var layer = LayerMask.LayerToName(other.gameObject.layer);
//         if (layer is not ("FenceTrigger" or "Tree" or "Statue" or "Stone")) return;
//
//         var leftInteractable = layer switch
//         {
//             "FenceTrigger" or "Tree" or "Statue" or "Stone" => true,
//             _ => false
//         };
//
//         if (leftInteractable)
//         {
//             woodTextComponent.SetText(Empty);
//             stoneTextComponent.SetText(Empty);
//         }
//     }
// }