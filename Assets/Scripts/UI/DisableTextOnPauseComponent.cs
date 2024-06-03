using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class DisableTextOnPauseComponent : MonoBehaviour
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
        textComponent.enabled = false;
    }

    private void OnGameStateResume()
    {
        textComponent.enabled = true;
    }

    private void OnDestroy()
    {
        GameStateHandlerComponent.OnGameStatePauseEntered -= OnGameStatePause;
        GameStateHandlerComponent.OnGameStateResumeEntered -= OnGameStateResume;
    }
}
