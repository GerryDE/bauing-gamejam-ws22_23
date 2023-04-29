using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // In welcher Welle befinden wir uns
    [SerializeField] int welle = 1;

    // In Welcher Generation befinden wir uns, ggf. als Pop-Up
    [SerializeField] int remainingYears;

    //Holz, und Steinvorrat
    [SerializeField] int anzahlStein;

    [SerializeField] int anzahlHolz;

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
    [SerializeField] TextMeshProUGUI tryAgainText;

    private bool fadeIn, fadeOut = false;
    [SerializeField] float fadeAmount = 0f;
    [SerializeField] float fadeSpeed;

    [SerializeField] GameObject prefabPopupText;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform parentTransform;
    private TextMeshPro textPopup;
    private bool _gameOverTriggered;

    private void Start()
    {
        ResourceData.OnCurrentWoodAmountChanged += UpdateHolzVorrat;
        ResourceData.OnCurrentStoneAmountChanged += UpdateSteinVorrat;
        PlayerData.OnPlayerCurrentRemainingYearsChanged += UpdateRemainingYears;
        DataHandlerComponent.OnWaveCountChanged += UpdateWaveCount;
        BossComponent.OnBossDestroyed += UpdateWelle;
        PlayerController.OnRestartGame += OnRestartGame;

        remainingYears = DataProvider.Instance.PlayerData.CurrentRemainingYears;

        InitTexteUndWerte();
        // gameOverText.enabled = false;
        // tryAgainText.enabled = false;
        textPopup = prefabPopupText.GetComponent<TextMeshPro>();
    }

    private void OnRestartGame()
    {
        if (!tryAgainText.enabled) return;
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
                    item.text = texte[index] + ": " + remainingYears;
                    index++;
                    break;
                case 1:
                    item.text = texte[index] + ": " + welle + "/3";
                    index++;
                    break;
                case 2:
                    item.text = texte[index] + ": " + anzahlHolz;
                    index++;
                    break;
                case 3:
                    item.text = texte[index] + ": " + anzahlStein;
                    index++;
                    break;
                default:
                    break;
            }
        }
    }

    private void UpdateWaveCount(int newValue)
    {
        welle = newValue + 1;
    }

    private void UpdateRemainingYears(int newValue)
    {
        remainingYears = newValue;
        if (remainingYears > 0 || _gameOverTriggered) return;
        _gameOverTriggered = true;
        StartCoroutine(EndGameScreen());
    }

    private void UpdateWelle()
    {
        welle++;
    }

    private void UpdateHolzVorrat(int setHolz)
    {
        anzahlHolz = setHolz;
    }

    private void UpdateSteinVorrat(int setStein)
    {
        anzahlStein = setStein;
    }

    private void OnDestroy()
    {
        ResourceData.OnCurrentWoodAmountChanged -= UpdateHolzVorrat;
        ResourceData.OnCurrentStoneAmountChanged -= UpdateSteinVorrat;
        PlayerData.OnPlayerCurrentRemainingYearsChanged -= UpdateRemainingYears;
        DataHandlerComponent.OnWaveCountChanged -= UpdateWaveCount;
        BossComponent.OnBossDestroyed -= UpdateWelle;
        PlayerController.OnRestartGame -= OnRestartGame;
    }

    IEnumerator EndGameScreen()
    {
        gameOverText.enabled = true;
        fadeIn = true;
        yield return new WaitForSeconds(2f);
        tryAgainText.enabled = true;
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

        Destroy(gameObject);
        yield break;
    }
}
