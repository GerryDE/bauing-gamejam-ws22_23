using System;
using UnityEngine;

public class CheckForGameOverComponent : MonoBehaviour
{
    public delegate void GameOver();
    public static GameOver OnGameOver;

    private void OnEnable()
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

    private void OnDisable()
    {
        DataProvider.OnCurrentRemainingYearsChanged -= OnCurrentRemainingYearsChanged;
    }
}