using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CreditSceneHandler : MonoBehaviour
{
    [Header("Scene to Switch to (String)")]
    [SerializeField] string mainMenuScene;
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(mainMenuScene);
        }
    }
}
