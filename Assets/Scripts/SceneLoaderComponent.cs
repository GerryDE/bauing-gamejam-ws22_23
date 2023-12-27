using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderComponent : MonoBehaviour
{
    [SerializeField] private string mainGame = "SampleScene";
    [SerializeField] private string credits = "Credits";
    [SerializeField] private string changelog = "Changelog";

    private void Start()
    {
        MenuInputHandlerComponent.OnStartGameTriggered += OnStartGameTriggered;
        MenuInputHandlerComponent.OnCreditsTriggered += OnCreditsTriggered;
        MenuInputHandlerComponent.OnChangelogTriggered += OnChangelogTriggered;
    }

    private void OnStartGameTriggered()
    {
        LoadScene(mainGame);
    }

    private void OnCreditsTriggered()
    {
        LoadScene(credits);
    }

    private void OnChangelogTriggered()
    {
        LoadScene(changelog);
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}