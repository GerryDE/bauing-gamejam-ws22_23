using Data;
using UnityEngine;

public class SkipTutorialHandlerComponent : MonoBehaviour
{
    [SerializeField] private SkipTutorialData skipTutorialData;

    public delegate void SkipTutorialValueToggled(bool value);

    public static SkipTutorialValueToggled OnSkipTutorialValueToggled;

    private void Start()
    {
        skipTutorialData.shallBeSkipped = false;
        MenuInputHandlerComponent.OnSkipTutorialTriggered += ToggleSkipTutorialValue;
    }

    private void ToggleSkipTutorialValue()
    {
        skipTutorialData.shallBeSkipped = !skipTutorialData.shallBeSkipped;
        OnSkipTutorialValueToggled?.Invoke(skipTutorialData.shallBeSkipped);
    }

    private void OnDestroy()
    {
        MenuInputHandlerComponent.OnSkipTutorialTriggered -= ToggleSkipTutorialValue;
    }
}