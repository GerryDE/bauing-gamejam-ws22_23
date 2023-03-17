using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{
    private void Awake()
    {
        TextMeshProUGUI textMeshProUGUI = gameObject.GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeOutText(5f, textMeshProUGUI));
    }

    IEnumerator FadeOutText(float fadeTime, TextMeshProUGUI textGameObject)
    {
        float amount;
        for (int i = 0; i < 40; i++)
        {
            amount = textGameObject.color.a - (fadeTime / 200);
            textGameObject.color = new Color(255, 255, 255, amount);
            yield return new WaitForSeconds(fadeTime / 100);
        }
        StartCoroutine(FadeInText(5f, textGameObject));
        yield break;
    }

    IEnumerator FadeInText(float fadeTime, TextMeshProUGUI textGameObject)
    {
        float amount;
        for (int i = 0; i < 40; i++)
        {
            amount = textGameObject.color.a + (fadeTime / 200);
            textGameObject.color = new Color(255, 255, 255, amount);
            yield return new WaitForSeconds(fadeTime / 100);
        }
        StartCoroutine(FadeOutText(5f, textGameObject));
        yield break;
    }

    private void OnSubmit()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void OnCancel()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
