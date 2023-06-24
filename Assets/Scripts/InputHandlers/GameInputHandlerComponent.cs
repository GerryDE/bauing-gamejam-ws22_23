using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputHandlerComponent : MonoBehaviour {

    public delegate void Pause();
    public static Pause OnPauseButtonPressed;

    public void OnPauseGame(InputValue value) {
        OnPauseButtonPressed?.Invoke();
    }
}
