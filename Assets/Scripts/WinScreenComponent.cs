using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenComponent : MonoBehaviour
{
    [SerializeField] private UnityEngine.Object mainMenuScene;
    [SerializeField] private UnityEngine.Object mainGameScene;

    private void Awake()
    {
        MenuInputHandlerComponent.OnExitButtonTriggered += OnExitButtonTriggered;
        MenuInputHandlerComponent.OnRestartButtonTriggered += OnRestartButtonTriggered;
    }

    private void OnExitButtonTriggered()
    {
        SceneManager.LoadScene(mainMenuScene.name);
    }

    private void OnRestartButtonTriggered()
    {
        SceneManager.LoadScene(mainGameScene.name);
    }
}
