using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using static System.String;

public class PlayerInteractionUiComponent : MonoBehaviour
{
    [SerializeField] private string buttonText;
    [SerializeField] private string fenceText;
    [SerializeField] private string treeText;
    [SerializeField] private string statueText;
    [SerializeField] private string mineText;

    [SerializeField] private TextMeshPro textComponent;

    private void Start()
    {
        textComponent.SetText(Empty);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var layer = LayerMask.LayerToName(col.gameObject.layer);
        if (layer is not ("FenceTrigger" or "Tree" or "Statue" or "Stone")) return;
        var action = layer switch
        {
            "FenceTrigger" => fenceText,
            "Tree" => treeText,
            "Statue" => statueText,
            "Stone" => mineText,
            _ => ""
        };

        textComponent.SetText("[" + buttonText + "] " + action);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var layer = LayerMask.LayerToName(other.gameObject.layer);
        if (layer is not ("FenceTrigger" or "Tree" or "Statue" or "Stone")) return;
        var leftInteractable = layer switch
        {
            "FenceTrigger" or "Tree" or "Statue" or "Stone" => true,
            _ => false
        };

        if (leftInteractable)
        {
            textComponent.SetText(Empty);
        }
    }
}