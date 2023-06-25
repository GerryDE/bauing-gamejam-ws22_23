using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class GameInputHandlerComponent : MonoBehaviour {

    PlayerInput _playerInput;

    public delegate void PauseButtonPressed();
    public delegate void ResumeButtonPressed();
    public static PauseButtonPressed OnPauseButtonPressed;
    public static ResumeButtonPressed OnResumeButtonPressed;

    private void Awake()
    {
        GameStateHandlerComponent.OnGameStatePause += OnGameStatePause;
        GameStateHandlerComponent.OnGameStateResume += OnGameStateResume;

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


    public void OnPauseGame(InputValue value) {
        OnPauseButtonPressed?.Invoke();
    }

    public void OnResumeGame(InputValue value)
    {
        OnResumeButtonPressed?.Invoke();
    }

    private void OnDestroy()
    {
        GameStateHandlerComponent.OnGameStatePause -= OnGameStatePause;
        GameStateHandlerComponent.OnGameStateResume -= OnGameStateResume;
    }
}
