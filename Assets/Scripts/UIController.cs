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
    [FormerlySerializedAs("generation")] [SerializeField] int alter;
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
        PlayerController.OnPlayerStoneQuarryInteraction += UpdateSteinVorrat;
        PlayerController.OnPlayerAgeChanged += UpdateAlter;
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
                    item.text = texte[index] + ": " + alter;
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

    public void UpdateAlter(int setAlter)
    {
        alter = setAlter;
    }

    public void UpdateWelle()
    {
        welle++;
    }

    public void UpdateHolzVorrat(int setHolz)
    {
        anzahlHolz = setHolz;
    }

    public void UpdateSteinVorrat(int setStein)
    {
        anzahlStein = setStein;
    }
}
