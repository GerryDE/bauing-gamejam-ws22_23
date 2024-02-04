using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
    [RequireComponent(typeof(CanvasGroup))]
    public class InteractableUiCanvasHandlerComponent : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] float NotEnoughResourcesAlpha = 0.5f;
        [SerializeField, Range(0f, 1f)] float EnoughResourcesAlpha = 1f;
        [SerializeField] Interactable Interactable;
        [SerializeField] bool Upgrade = true;

        private CanvasGroup _canvasGroup;

        private void OnEnable()
        {
            var data = DataProvider.Instance;
            _canvasGroup = GetComponent<CanvasGroup>();

            int version = 0;
            int count = 0;
            switch (Interactable)
            {
                case Interactable.Fence_0_Repair:
                case Interactable.Fence_0_Upgrade:
                    version = data.GetCurrentFenceVersion(0);
                    count = data.FenceData[0].data.Count;
                    break;
                case Interactable.Fence_1_Repair:
                case Interactable.Fence_1_Upgrade:
                    version = data.GetCurrentFenceVersion(1);
                    count = data.FenceData[1].data.Count;
                    break;
                case Interactable.Tree_Upgrade:
                    version = data.CurrentTreeVersion;
                    count = data.TreeData.Count;
                    break;
                case Interactable.Stone_Upgrade:
                    version = data.CurrentMineVersion;
                    count = data.MineData.Count;
                    break;
                case Interactable.Statue_Upgrade:
                    version = data.CurrentStatueVersion;
                    count = -1; // use -1 when no max version exists
                    break;
            }

            if (Upgrade)
            {
                version++;
            }

            if (version < count || count == -1)
            {
                int currentLumberAmount = data.ResourceData.WoodAmount;
                int currentStoneAmount = data.ResourceData.StoneAmount;
                int requiredLumberAmount = GetCostData(version).lumberCost;
                int requiredStoneAmount = GetCostData(version).stoneCost;

                if (currentLumberAmount >= requiredLumberAmount && currentStoneAmount >= requiredStoneAmount)
                {
                    _canvasGroup.alpha = EnoughResourcesAlpha;
                }
                else
                {
                    _canvasGroup.alpha = NotEnoughResourcesAlpha;
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private CostData GetCostData(int version)
        {
            CostData costData = null;
            var data = DataProvider.Instance;
            switch (Interactable)
            {
                case Interactable.Fence_0_Repair:
                    costData = data.FenceData[0].data[version].repairCost;
                    break;
                case Interactable.Fence_0_Upgrade:
                    costData = data.FenceData[0].data[version].upgradeCost;
                    break;
                case Interactable.Fence_1_Repair:
                    costData = data.FenceData[1].data[version].repairCost;
                    break;
                case Interactable.Fence_1_Upgrade:
                    costData = data.FenceData[1].data[version].upgradeCost;
                    break;
                case Interactable.Tree_Upgrade:
                    costData = data.TreeData[version].upgradeCost;
                    break;
                case Interactable.Stone_Upgrade:
                    costData = data.MineData[version].upgradeCost;
                    break;
                case Interactable.Statue_Upgrade:
                    costData = data.NextStatueData.upgradeCost;
                    break;
            }

            return costData;
        }
    }
}