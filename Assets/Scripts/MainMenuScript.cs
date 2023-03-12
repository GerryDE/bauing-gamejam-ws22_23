using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] String scene;
    
    public void SwitchToGame()
    {
        SceneManager.LoadScene(scene);
    }
}
