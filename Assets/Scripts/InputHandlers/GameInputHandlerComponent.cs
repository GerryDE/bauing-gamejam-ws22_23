using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class GameInputHandlerComponent : MonoBehaviour {

    PlayerInput _playerInput;

    public delegate void Pause();
    public static Pause OnPauseButtonPressed;

    private void Awake()
    {
        GameStateHandlerComponent.OnGameStateChanged += OnGameStateChanged;

        _playerInput = gameObject.GetComponent<PlayerInput>();
    }

    private void OnGameStateChanged(GameStateHandlerComponent.GameState gameState)
    {
        if (gameState == GameStateHandlerComponent.GameState.PAUSED)
        {
            _playerInput.SwitchCurrentActionMap("Pause");
        }
        else if (gameState == GameStateHandlerComponent.GameState.RUNNING)
        {
            _playerInput.SwitchCurrentActionMap("Player");
        }
    }


    public void OnPauseGame(InputValue value) {
        Debug.Log("Pause button pressed");
        OnPauseButtonPressed?.Invoke();
    }

    public void OnResumeGame(InputValue value)
    {
        Debug.Log("Resume button pressed");
        OnPauseButtonPressed?.Invoke();
    }
}
