using TMPro;
using UnityEngine;
using static System.String;

public class PlayerInteractionUiComponent : MonoBehaviour
{
    [SerializeField] protected string buttonText;
    [SerializeField] protected string fenceText;
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

        textComponent.SetText("[" + buttonText + " (Hold)] " + action);

        if (buttonText.Equals("E"))
        {
            textComponent.SetText("[" + buttonText + " (Press)] " + action);
        }

        if (layer is "FenceTrigger" && buttonText != "E")
        {
            textComponent.SetText(Empty);
        }

        if (layer != "Tree") return;
        var state = other.gameObject.GetComponent<TreeComponent>().GetState();
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