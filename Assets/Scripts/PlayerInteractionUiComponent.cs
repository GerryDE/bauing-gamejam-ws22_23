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

    [SerializeField] protected TextMeshPro textComponent;

    protected virtual void Start()
    {
        textComponent.SetText(Empty);
    }

    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        var layer = LayerMask.LayerToName(other.gameObject.layer);
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

        if (layer != "Tree") return;
        var state = other.gameObject.GetComponent<TreeComponent>().getState();
        if (state == TreeComponent.State.Spawning)
        {
            textComponent.SetText(Empty);
        }
        
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
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