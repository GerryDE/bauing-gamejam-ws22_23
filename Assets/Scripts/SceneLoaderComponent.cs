using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderComponent : MonoBehaviour
{
    [SerializeField] private SceneAsset mainGame;
    [SerializeField] private SceneAsset credits;
    [SerializeField] private SceneAsset changelog;

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

    private void LoadScene(SceneAsset sceneAsset)
    {
        SceneManager.LoadScene(sceneAsset.name);
    }
}