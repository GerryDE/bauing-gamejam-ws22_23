using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class MenuInputHandlerComponent : MonoBehaviour
{
    private PlayerInput _playerInput;

    public delegate void ScrollbarButtonTriggered(float value);

    public delegate void ExitButtonTriggered();

    public delegate void RestartButtonTriggered();

    public delegate void SkipTutorialTriggered();

    public delegate void CreditsTriggered();

    public delegate void ChangelogTriggered();

    public delegate void StartGameTriggered();

    public static ScrollbarButtonTriggered OnScrollbarButtonTriggered;
    public static ExitButtonTriggered OnExitButtonTriggered;
    public static RestartButtonTriggered OnRestartButtonTriggered;
    public static SkipTutorialTriggered OnSkipTutorialTriggered;
    public static CreditsTriggered OnCreditsTriggered;
    public static ChangelogTriggered OnChangelogTriggered;
    public static StartGameTriggered OnStartGameTriggered;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    public void OnScollbar(InputValue value)
    {
        OnScrollbarButtonTriggered?.Invoke(value.Get<float>());
    }

    public void OnExit(InputValue value)
    {
        OnExitButtonTriggered?.Invoke();
    }

    public void OnRestart(InputValue value)
    {
        OnRestartButtonTriggered?.Invoke();
    }

    public void OnSkipTutorial(InputValue value)
    {
        OnSkipTutorialTriggered?.Invoke();
    }

    public void OnCredits(InputValue value)
    {
        OnCreditsTriggered?.Invoke();
    }

    public void OnChangelog(InputValue value)
    {
        OnChangelogTriggered?.Invoke();
    }

    public void OnStartGame(InputValue value)
    {
        OnStartGameTriggered?.Invoke();
    }
}