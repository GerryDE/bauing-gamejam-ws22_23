using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] String gameScene;
    [SerializeField] String changelogScene;
    [SerializeField] String creditScene;

    // Check for Button-Presses
    public void OnSubmit()
    {
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(changelogScene);
        }
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(creditScene);
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(gameScene);
        }
    }
}
