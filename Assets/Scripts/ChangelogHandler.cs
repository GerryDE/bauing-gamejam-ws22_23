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

    //Check for Button-presses
    private void Update()
    {
        if ((Keyboard.current.sKey.wasPressedThisFrame || Keyboard.current.sKey.isPressed) && scrollbar.value < 1f)
        {
            //Scroll-Down
            scrollbar.value += scrollSpeed;
        }
        if ((Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.wKey.isPressed) && scrollbar.value > 0f)
        {
            //Scroll-Up
            scrollbar.value -= scrollSpeed;
        }
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            //Load Back to MainMenu
            SceneManager.LoadScene(mainMenuScene);
        }
    }

    private void Awake()
    {
        LoadChangelogText();
        LoadScrollbarLength();
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
}
