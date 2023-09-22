using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class EnableTextOnPauseComponent : MonoBehaviour
{
    private TextMeshPro textComponent;

    private void Awake()
    {
        GameStateHandlerComponent.OnGameStatePauseEntered += OnGameStatePause;
        GameStateHandlerComponent.OnGameStateResumeEntered += OnGameStateResume;

        textComponent = GetComponent<TextMeshPro>();
    }

    private void OnGameStatePause()
    {
        textComponent.enabled = true;
    }

    private void OnGameStateResume()
    {
        textComponent.enabled = false;
    }

    private void OnDestroy()
    {
        GameStateHandlerComponent.OnGameStatePauseEntered -= OnGameStatePause;
        GameStateHandlerComponent.OnGameStateResumeEntered -= OnGameStateResume;
    }
}
