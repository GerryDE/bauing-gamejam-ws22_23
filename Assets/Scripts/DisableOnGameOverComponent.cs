using System;
using UnityEngine;

public class DisableOnGameOverComponent : MonoBehaviour
{
    private void Awake()
    {
        CheckForGameOverComponent.OnGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        if (this == null) return;
        gameObject.SetActive(false);
    }
}