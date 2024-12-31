using System;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class CurrentResourceTextComponent : MonoBehaviour
    {
        private enum ResourceType { Lumber, Stone };

        [SerializeField] private ResourceType resourceType;

        private TextMeshProUGUI _textComponent;

        public void OnEnable() 
        {
            _textComponent = GetComponent<TextMeshProUGUI>();

            DataProvider.OnResourceDataChanged += OnResourceDataChanged;
        }

        private void OnResourceDataChanged(DataProvider.CurrentResourceData data)
        {
            if (resourceType == ResourceType.Lumber) {
                _textComponent.text = data.WoodAmount.ToString();
            } else if (resourceType == ResourceType.Stone) {
                _textComponent.text = data.StoneAmount.ToString();
            }
        }
    }
}