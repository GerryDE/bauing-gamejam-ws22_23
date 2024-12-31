using UnityEngine;
using static GameStateHandlerComponent;

public class TimeScaleHandlerComponent : MonoBehaviour
{
    private void Awake()
    {
        OnGameStatePauseEntered += OnGameStatePause;
        OnGameStateResumeEntered += OnGameStateResume;
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
        OnGameStatePauseEntered -= OnGameStatePause;
        OnGameStateResumeEntered -= OnGameStateResume;
    }
}