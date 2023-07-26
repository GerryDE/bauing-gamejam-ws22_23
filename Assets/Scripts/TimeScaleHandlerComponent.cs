using UnityEngine;
using static GameStateHandlerComponent;

public class TimeScaleHandlerComponent : MonoBehaviour {

    private void Awake() {
        GameStateHandlerComponent.OnGameStatePauseEntered += OnGameStatePause;
        GameStateHandlerComponent.OnGameStateResumeEntered += OnGameStateResume;
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
        GameStateHandlerComponent.OnGameStatePauseEntered -= OnGameStatePause;
        GameStateHandlerComponent.OnGameStateResumeEntered -= OnGameStateResume;
    }
}
