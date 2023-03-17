using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] String scene;
    
    public void OnSubmit()
    {
        SceneManager.LoadScene(scene);
    }
}
