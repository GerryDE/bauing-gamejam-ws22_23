using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangelogHandler : MonoBehaviour
{
    [Header("Scrollbar")]
    [SerializeField] float scrollSpeed;
    [SerializeField] Scrollbar scrollbar;
    [Header("Text Asset")]
    [SerializeField] TextMeshProUGUI changelogText;
    [Header("Canvas")]
    [SerializeField] Canvas canvas;
    [Header("Changelog .txt File")]
    [SerializeField] TextAsset textAsset;
    [Header("Scene to Load to (Main Menu)")]
    [SerializeField] string mainMenuScene;

    private void Awake()
    {
        MenuInputHandlerComponent.OnScrollbarButtonTriggered += OnScrollbarButtonTriggered;
        MenuInputHandlerComponent.OnExitButtonTriggered += OnExitButtonTriggered;

        LoadChangelogText();
        LoadScrollbarLength();
    }

    private void OnScrollbarButtonTriggered(float value)
    {
        //Scroll-Up/Down
        scrollbar.value += value;
    }

    private void OnExitButtonTriggered()
    {
        //Load Back to MainMenu
        SceneManager.LoadScene(mainMenuScene);
    }

    //Load Scrollbar Length, if Text is Longer than Canvas
    private void LoadScrollbarLength()
    {
        scrollbar.size = canvas.pixelRect.size.y / changelogText.rectTransform.sizeDelta.y;
    }

    //Load Text from Changelog into Scene from Data
    private void LoadChangelogText()
    {
        changelogText.text = textAsset.text;
    }

    private void OnDestroy()
    {
        MenuInputHandlerComponent.OnScrollbarButtonTriggered -= OnScrollbarButtonTriggered;
        MenuInputHandlerComponent.OnExitButtonTriggered -= OnExitButtonTriggered;
    }
}
