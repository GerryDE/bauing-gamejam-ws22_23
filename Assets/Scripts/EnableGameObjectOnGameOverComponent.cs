using System;
using UnityEngine;

public class EnableGameObjectOnGameOverComponent : MonoBehaviour
{
    [SerializeField] private GameObject obj;

    private void OnEnable()
    {
        CheckForGameOverComponent.OnGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        obj.SetActive(true);
    }

    private void OnDisable()
    {
        CheckForGameOverComponent.OnGameOver -= OnGameOver;
    }
}