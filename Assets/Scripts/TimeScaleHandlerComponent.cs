using UnityEngine;
using static GameStateHandlerComponent;

public class TimeScaleHandlerComponent : MonoBehaviour {

    private void Awake() {
        GameStateHandlerComponent.OnGameStatePause += OnGameStatePause;
        GameStateHandlerComponent.OnGameStateResume += OnGameStateResume;
    }

    private void OnGameStatePause()
    {
        Time.timeScale = 0f;
    }

    private void OnGameStateResume()
    {
        Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        GameStateHandlerComponent.OnGameStatePause -= OnGameStatePause;
        GameStateHandlerComponent.OnGameStateResume -= OnGameStateResume;
    }
}
