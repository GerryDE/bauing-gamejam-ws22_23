using System;
using UnityEngine;

public class DisableOnGameOverComponent : MonoBehaviour
{
    private void OnEnable()
    {
        CheckForGameOverComponent.OnGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        if (this == null) return;
        gameObject.SetActive(false);
    }

    private void OnDisable() {
        CheckForGameOverComponent.OnGameOver -= OnGameOver;
    }
}