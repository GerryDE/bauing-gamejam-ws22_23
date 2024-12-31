using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnableChildOnPauseComponent : MonoBehaviour
{
    [SerializeField] private GameObject obj;

    private void OnEnable()
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

    private void OnDisable()
    {
        GameStateHandlerComponent.OnGameStatePauseEntered -= OnGameStatePause;
        GameStateHandlerComponent.OnGameStateResumeEntered -= OnGameStateResume;
    }
}
