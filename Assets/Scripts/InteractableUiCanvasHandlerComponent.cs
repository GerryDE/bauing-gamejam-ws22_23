using System;
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

        private DataProvider _dataProvider;
        private DataHandlerComponent _dataHandlerComponent;
        private CanvasGroup _canvasGroup;

        private void OnEnable()
        {
            _dataProvider = DataProvider.Instance;
            _dataHandlerComponent = GameObject.FindWithTag("DataHandler").GetComponent<DataHandlerComponent>();
            _canvasGroup = GetComponent<CanvasGroup>();

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

            if (Upgrade)
            {
                version++;
            }

            if (version < count)
            {
                int currentLumberAmount = _dataProvider.ResourceData.WoodAmount;
                int currentStoneAmount = _dataProvider.ResourceData.StoneAmount;
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
            CostData data = null;
            switch (Interactable)
            {
                case Interactable.Fence_Repair:
                    data = _dataProvider.FenceData[version].repairCost;
                    break;
                case Interactable.Fence_Upgrade:
                    data = _dataProvider.FenceData[version].upgradeCost;
                    break;
                case Interactable.Tree_Upgrade:
                    data = _dataProvider.TreeData[version].upgradeCost;
                    break;
                case Interactable.Stone_Upgrade:
                    data = _dataProvider.MineData[version].upgradeCost;
                    break;
            }

            return data;
        }
    }
}
