using System;
using UnityEngine;

public class DestroyOnGameOverComponent : MonoBehaviour
{
    private void OnEnable()
    {
        CheckForGameOverComponent.OnGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        Destroy(this);
    }

    private void OnDisable() {
        CheckForGameOverComponent.OnGameOver -= OnGameOver;
    }
}