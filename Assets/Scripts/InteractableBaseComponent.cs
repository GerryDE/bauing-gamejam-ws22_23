using UnityEngine;

public class InteractableBaseComponent : MonoBehaviour
{
    protected DataHandlerComponent _dataHandlerComponent;
    protected bool _interactionButton1Holding;
    protected bool _interactionButton1Pressed;
    protected bool _interaction1Enabled;

    protected virtual void Start()
    {
        PlayerController.OnInteractionButton1Hold += OnInteractionButton1Hold;
        PlayerController.OnInteractionButton1Released += OnInteractionButton1Released;
        PlayerController.OnInteractionButton1Pressed += OnInteractionButton1Pressed;

        _dataHandlerComponent = GameObject.FindWithTag("DataHandler").GetComponent<DataHandlerComponent>();
    }

    private void OnInteractionButton1Pressed()
    {
        _interactionButton1Pressed = true;
    }

    protected virtual void OnInteractionButton1Hold()
    {
        _interactionButton1Holding = true;
    }

    protected virtual void OnInteractionButton1Released()
    {
        _interactionButton1Holding = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Player")) && _interactionButton1Holding)
        {
            _interaction1Enabled = true;
        }
        else
        {
            _interaction1Enabled = false;
        }
    }
}