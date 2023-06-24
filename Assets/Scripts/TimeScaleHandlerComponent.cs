using UnityEngine;
using static GameStateHandlerComponent;

public class TimeScaleHandlerComponent : MonoBehaviour {

    private void Awake() {
        GameStateHandlerComponent.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState gameState) {
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
        GameStateHandlerComponent.OnGameStateChanged -= OnGameStateChanged;
    }
}
