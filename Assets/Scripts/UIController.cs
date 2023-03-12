using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Serialization;

public class UIController : MonoBehaviour
{
    // In welcher Welle befinden wir uns
    [SerializeField] int welle = 1;
    // In Welcher Generation befinden wir uns, ggf. als Pop-Up
    [FormerlySerializedAs("alter")] [FormerlySerializedAs("generation")] [SerializeField] int remainingYears;
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

    private void Awake()
    {
        DataHandlerComponent.OnWoodAmountChanged += UpdateHolzVorrat;
        DataHandlerComponent.OnStoneAmountChanged += UpdateSteinVorrat;
        DataHandlerComponent.OnRemainingYearsChanged += UpdateRemainingYears;
        BossComponent.OnBossDestroyed += UpdateWelle;
        InitTexteUndWerte();
    }

    private void Update()
    {
        InitTexteUndWerte();
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
                    item.text = texte[index] + ": " + welle;
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

    private void UpdateRemainingYears(int newValue)
    {
        remainingYears = newValue;
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
        DataHandlerComponent.OnWoodAmountChanged -= UpdateHolzVorrat;
        DataHandlerComponent.OnStoneAmountChanged -= UpdateSteinVorrat;
        DataHandlerComponent.OnRemainingYearsChanged += UpdateRemainingYears;
        BossComponent.OnBossDestroyed -= UpdateWelle;
    }
}
