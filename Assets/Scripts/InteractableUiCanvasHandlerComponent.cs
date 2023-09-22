﻿using System;
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
                case Interactable.Fence_Repair:
                case Interactable.Fence_Upgrade:
                    version = data.CurrentFenceVersion;
                    count = data.FenceData.Count;
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
                    count = data.StatueData.Count;
                    break;
            }

            if (Upgrade)
            {
                version++;
            }

            if (version < count)
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
                case Interactable.Fence_Repair:
                    costData = data.FenceData[version].repairCost;
                    break;
                case Interactable.Fence_Upgrade:
                    costData = data.FenceData[version].upgradeCost;
                    break;
                case Interactable.Tree_Upgrade:
                    costData = data.TreeData[version].upgradeCost;
                    break;
                case Interactable.Stone_Upgrade:
                    costData = data.MineData[version].upgradeCost;
                    break;
                case Interactable.Statue_Upgrade:
                    costData = data.StatueData[version].upgradeCost;
                    break;
            }

            return costData;
        }
    }
}