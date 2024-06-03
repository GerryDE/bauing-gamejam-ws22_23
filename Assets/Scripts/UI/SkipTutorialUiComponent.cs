using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class SkipTutorialUiComponent : MonoBehaviour
    {
        private Image _image;

        private void Start()
        {
            _image = GetComponent<Image>();
            SkipTutorialHandlerComponent.OnSkipTutorialValueToggled += OnSkipTutorialValueToggled;
        }

        private void OnSkipTutorialValueToggled(bool value)
        {
            _image.enabled = value;
        }

        private void OnDestroy()
        {
            SkipTutorialHandlerComponent.OnSkipTutorialValueToggled -= OnSkipTutorialValueToggled;
        }
    }
}