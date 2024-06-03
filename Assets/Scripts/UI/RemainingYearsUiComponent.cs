using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Slider))]
    public class RemainingYearsUiComponent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentRemainingYearsText;
        [SerializeField] private TextMeshProUGUI maxRemainingYearsText;

        private Slider _slider;

        private void Start()
        {
            _slider = GetComponent<Slider>();
            UpdateComponents(0);

            DataProvider.OnCurrentRemainingYearsChanged += UpdateComponents;
            DataProvider.OnPlayerMaxRemainingYearsChanged += UpdateComponents;
        }

        private void UpdateComponents(int value)
        {
            var playerData = DataProvider.Instance.PlayerData;
            currentRemainingYearsText.SetText(playerData.CurrentRemainingYears.ToString());
            maxRemainingYearsText.SetText(playerData.MaxRemainingYears.ToString());
            _slider.value = playerData.CurrentRemainingYears / (float)playerData.MaxRemainingYears;
        }

        private void OnDestroy()
        {
            DataProvider.OnCurrentRemainingYearsChanged -= UpdateComponents;
            DataProvider.OnPlayerMaxRemainingYearsChanged -= UpdateComponents;
        }
    }
}
