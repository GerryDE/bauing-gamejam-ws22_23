using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    // In welcher Welle befinden wir uns
    [SerializeField] int welle;
    // In Welcher Generation befinden wir uns, ggf. als Pop-Up
    [SerializeField] int generation;
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
        PlayerController.OnPlayerTreeInteraction += UpdateHolzVorrat;
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
                    item.text = texte[index] + ": " + generation;
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

    public void UpdateGeneration()
    {
        generation++;
    }

    public void UpdateWelle()
    {
        welle++;
    }

    public void UpdateHolzVorrat(int addHolz)
    {
        anzahlHolz += addHolz;
    }

    public void UpdateSteinVorrat(int addStein)
    {
        anzahlStein += addStein;
    }
}
