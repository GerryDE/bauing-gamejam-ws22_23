using System;
using UnityEngine;

public class CheckForGameOverComponent : MonoBehaviour
{
    public delegate void GameOver();
    public static GameOver OnGameOver;

    private void Awake()
    {
        DataProvider.OnCurrentRemainingYearsChanged += OnCurrentRemainingYearsChanged;
    }

    private void OnCurrentRemainingYearsChanged(int value)
    {
        if (value <= 0)
        {
            OnGameOver?.Invoke();
        }
    }

    private void OnDestroy()
    {
        DataProvider.OnCurrentRemainingYearsChanged -= OnCurrentRemainingYearsChanged;
    }
}