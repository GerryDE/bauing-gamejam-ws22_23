using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CreditSceneHandler : MonoBehaviour
{
    [Header("Scene to Switch to (String)")]
    [SerializeField] string mainMenuScene;

    private void Awake()
    {
        MenuInputHandlerComponent.OnExitButtonTriggered += OnExitButtonTriggered;
    }

    private void OnExitButtonTriggered()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    private void OnDestroy()
    {
        MenuInputHandlerComponent.OnExitButtonTriggered -= OnExitButtonTriggered;
    }
}
