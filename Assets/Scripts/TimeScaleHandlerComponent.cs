using UnityEngine;
using static GameStateHandlerComponent;

public class TimeScaleHandlerComponent : MonoBehaviour {

    private void Awake() {
        GameInputHandlerComponent.OnPauseButtonPressed += OnPause;
    }

    private void OnPause() {
        GameState gameState = Instance.GetGameState();
        if (gameState == GameState.PAUSED)
        {
            Time.timeScale = 0f;
        } 
        else if (gameState == GameState.RUNNING)
        {
            Time.timeScale = 1f;
        }
    }

    private void OnDestroy()
    {
        GameInputHandlerComponent.OnPauseButtonPressed -= OnPause;
    }
}
