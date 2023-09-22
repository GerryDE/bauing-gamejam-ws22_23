using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class GameInputHandlerComponent : MonoBehaviour {

    PlayerInput _playerInput;

    // Player
    public delegate void MoveCalled(float velocity);
    public delegate void Interact1HoldCalled();
    public delegate void Interact1ReleasedCalled();
    public delegate void Interact1PressCalled();
    public delegate void Interact2PressCalled();
    public delegate void RestartCalled();
    public delegate void PauseCalled();

    public static MoveCalled OnMoveCalled;
    public static Interact1HoldCalled OnInteract1HoldCalled;
    public static Interact1ReleasedCalled OnInteract1ReleasedCalled;
    public static Interact1PressCalled OnInteract1PressCalled;
    public static Interact2PressCalled OnInteract2PressCalled;
    public static RestartCalled OnRestartCalled;
    public static PauseCalled OnPauseCalled;

    // Pause
    public delegate void ResumeCalled();

    public static ResumeCalled OnResumeCalled;

    private void Awake()
    {
        GameStateHandlerComponent.OnGameStatePauseEntered += OnGameStatePause;
        GameStateHandlerComponent.OnGameStateResumeEntered += OnGameStateResume;

        _playerInput = gameObject.GetComponent<PlayerInput>();
    }

    private void OnGameStatePause()
    {
        _playerInput.SwitchCurrentActionMap("Pause");
    }

    private void OnGameStateResume()
    {
        _playerInput.SwitchCurrentActionMap("Player");
    }


    public void OnPauseGame(InputValue value) 
    {
        OnPauseCalled?.Invoke();
    }

    public void OnResumeGame(InputValue value)
    {
        OnResumeCalled?.Invoke();
    }

    public void OnMove(InputValue value)
    {
        OnMoveCalled?.Invoke(value.Get<float>());
    }

    public void OnInteract1Press(InputValue value)
    {
        var floatValue = value.Get<float>();
        if (floatValue > 0f)
        {
            OnInteract1PressCalled?.Invoke();
        }
    }

    public void OnInteract1Hold(InputValue value)
    {
        var floatValue = value.Get<float>();
        if (floatValue > 0f)
        {
            OnInteract1HoldCalled?.Invoke();
        }
        else
        {
            OnInteract1ReleasedCalled?.Invoke();
        }
    }

    public void OnInteract2Press(InputValue value)
    {
        var floatValue = value.Get<float>();
        if (floatValue > 0f)
        {
            OnInteract2PressCalled?.Invoke();
        }
    }

    public void OnRestart(InputValue value)
    {
        OnRestartCalled.Invoke();
    }

    private void OnDestroy()
    {
        GameStateHandlerComponent.OnGameStatePauseEntered -= OnGameStatePause;
        GameStateHandlerComponent.OnGameStateResumeEntered -= OnGameStateResume;
    }
}
