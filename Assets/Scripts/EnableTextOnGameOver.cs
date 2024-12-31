using System;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class EnableTextOnGameOver : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void OnEnable()
    {
        CheckForGameOverComponent.OnGameOver += OnGameOver;

        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnGameOver()
    {
        _text.enabled = true;
    }

    private void OnDisable()
    {
        CheckForGameOverComponent.OnGameOver -= OnGameOver;
    }
}