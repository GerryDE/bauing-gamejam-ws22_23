using Data;
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
            switch (DataProvider.Instance.NextStatueData.statToUpgrade)
            {
                case StatueData.UpgradeableStat.MaxHp:
                    _textComponent.text = maxRemainingYearsText;
                    break;
                case StatueData.UpgradeableStat.Atk:
                    _textComponent.text = atkText;
                    break;
                case StatueData.UpgradeableStat.Def:
                    _textComponent.text = defText;
                    break;
                case StatueData.UpgradeableStat.Speed:
                    _textComponent.text = speedText;
                    break;
                default:
                    _textComponent.text = "<Unknown stat type>";
                    break;
            }
        }
    }
}