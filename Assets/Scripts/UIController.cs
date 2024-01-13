using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    //Liste der Texte
    [SerializeField] List<TextMeshProUGUI> listTexte = new List<TextMeshProUGUI>();
    // Nach Index was ist was
    // -(0) Generation
    // -(1) Welle
    // -(2) Holz
    // -(3) Stein

    // Initiale Texte
    [SerializeField] String[] texte;

    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] GameObject tryAgainTextObj;

    private bool fadeIn = false;
    [SerializeField] float fadeAmount = 0f;
    [SerializeField] float fadeSpeed;

    [SerializeField] GameObject prefabPopupText;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform parentTransform;
    private TextMeshPro textPopup;
    private bool _gameOverTriggered;

    private static DataProvider.CurrentPlayerData _playerData;
    private static DataProvider.CurrentResourceData _resourceData;

    private void Start()
    {
        _playerData = DataProvider.Instance.PlayerData;
        _resourceData = DataProvider.Instance.ResourceData;

        //BossComponent.OnBossDestroyed += UpdateWelle;
        GameInputHandlerComponent.OnRestartCalled += OnRestartGame;
        CheckForGameOverComponent.OnGameOver += OnGameOver;

        InitTexteUndWerte();
        // gameOverText.enabled = false;
        // tryAgainText.enabled = false;
        textPopup = prefabPopupText.GetComponent<TextMeshPro>();
    }

    private void OnGameOver()
    {
        if (_gameOverTriggered) return;
        _gameOverTriggered = true;
        StartCoroutine(EndGameScreen());
    }

    private void OnRestartGame()
    {
        if (!tryAgainTextObj.activeSelf) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        InitTexteUndWerte();

        if (fadeIn)
        {
            fadeAmount = gameOverText.color.a + (fadeSpeed * Time.deltaTime);
            gameOverText.color = new Color(255, 255, 255, fadeAmount);
            if (gameOverText.color.a >= 255)
            {
                fadeIn = false;
            }
        }
    }

    public void giveFeedbackWithValues(int value, String text)
    {
        textPopup.text = text + " +" + value;
        var positionNew = playerTransform.position;
        GameObject instantiatetdText = Instantiate(prefabPopupText, positionNew, Quaternion.identity, parentTransform);
        TextMeshPro textMeshProUGUI = instantiatetdText.GetComponent<TextMeshPro>();
        StartCoroutine(FadeOutText(1f, textMeshProUGUI, instantiatetdText));
    }

    private void InitTexteUndWerte()
    {
        int index = 0;
        foreach (var item in listTexte)
        {
            switch (index)
            {
                case 0:
                    item.text = texte[index] + ": " + _playerData.CurrentRemainingYears;
                    index++;
                    break;
                case 1:
                    item.text = texte[index] + ": " + DataProvider.Instance.Wave + "/3";
                    index++;
                    break;
                case 2:
                    item.text = texte[index] + ": " + _resourceData.WoodAmount;
                    index++;
                    break;
                case 3:
                    item.text = texte[index] + ": " + _resourceData.StoneAmount;
                    index++;
                    break;
                default:
                    break;
            }
        }
    }

    private void OnDestroy()
    {
        GameInputHandlerComponent.OnRestartCalled -= OnRestartGame;
    }

    IEnumerator EndGameScreen()
    {
        gameOverText.enabled = true;
        fadeIn = true;
        yield return new WaitForSeconds(2f);
        tryAgainTextObj.SetActive(true);
    }

    IEnumerator FadeOutText(float fadeTime, TextMeshPro textGameObject, GameObject gameObject)
    {
        float amount;
        for (int i = 0; i < 100; i++)
        {
            amount = textGameObject.color.a - (fadeTime / 40);
            textGameObject.color = new Color(255, 255, 255, amount);
            yield return new WaitForSeconds(fadeTime / 10000);
        }

        //Destroy(gameObject);
        yield break;
    }
}