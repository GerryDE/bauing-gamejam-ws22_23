using System;
using Data;
using Data.upgradeable_objects.statue;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class StatueUiNextLevelTextComponent : MonoBehaviour
    {
        [SerializeField] private string maxRemainingYearsText = "Max rem. years";
        [SerializeField] private string atkText = "Attack";
        [SerializeField] private string defText = "Defense";
        [SerializeField] private string speedText = "Move speed";

        private TextMeshProUGUI _textComponent;

        private void Awake()
        {
            _textComponent = GetComponent<TextMeshProUGUI>();
            UpdateText();

            StatueUpgradeComponent.OnUpgradeStatue += OnUpgradeStatue;
        }

        private void OnUpgradeStatue(StatueData.UpgradeableStat stat, float value)
        {
            UpdateText();
        }

        private void OnEnable()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            _textComponent.text = DataProvider.Instance.NextStatueData.statToUpgrade switch
            {
                StatueData.UpgradeableStat.MaxHp => maxRemainingYearsText,
                StatueData.UpgradeableStat.Atk => atkText,
                StatueData.UpgradeableStat.Def => defText,
                StatueData.UpgradeableStat.Speed => speedText,
                _ => "<Unknown stat type>"
            };
        }

        private void OnDestroy()
        {
            StatueUpgradeComponent.OnUpgradeStatue -= OnUpgradeStatue;
        }
    }
}