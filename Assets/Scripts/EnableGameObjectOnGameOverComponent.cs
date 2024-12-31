using System;
using UnityEngine;

public class EnableGameObjectOnGameOverComponent : MonoBehaviour
{
    [SerializeField] private GameObject obj;

    private void Awake()
    {
        CheckForGameOverComponent.OnGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        obj.SetActive(true);
    }

    private void OnDestroy()
    {
        CheckForGameOverComponent.OnGameOver -= OnGameOver;
    }
}