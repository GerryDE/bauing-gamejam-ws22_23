using System;
using UnityEngine;

public class DestroyOnGameOverComponent : MonoBehaviour
{
    private void Awake()
    {
        CheckForGameOverComponent.OnGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        Destroy(this);
    }
}