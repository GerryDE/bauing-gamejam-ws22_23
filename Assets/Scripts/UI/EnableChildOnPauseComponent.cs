using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnableChildOnPauseComponent : MonoBehaviour
{
    [SerializeField] private GameObject obj;

    private void Awake()
    {
        GameStateHandlerComponent.OnGameStatePauseEntered += OnGameStatePause;
        GameStateHandlerComponent.OnGameStateResumeEntered += OnGameStateResume;
    }

    private void OnGameStatePause()
    {
        obj.SetActive(true);
    }

    private void OnGameStateResume()
    {
        obj.SetActive(false);
    }

    private void OnDestroy()
    {
        GameStateHandlerComponent.OnGameStatePauseEntered -= OnGameStatePause;
        GameStateHandlerComponent.OnGameStateResumeEntered -= OnGameStateResume;
    }
}
