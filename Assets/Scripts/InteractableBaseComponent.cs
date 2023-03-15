using UnityEngine;

public class InteractableBaseComponent : MonoBehaviour
{
    protected DataHandlerComponent _dataHandlerComponent;
    protected bool _interactionButton1Holding;
    protected bool _interactionButton1Pressed;
    protected bool _interactionButton2Pressed;
    protected bool _interaction1Enabled;
    protected bool _interaction2Enabled;
    protected bool _isCollidingWithPlayer;

    protected virtual void Start()
    {
        PlayerController.OnInteractionButton1Hold += OnInteractionButton1Hold;
        PlayerController.OnInteractionButton1Released += OnInteractionButton1Released;
        PlayerController.OnInteractionButton1Pressed += OnInteractionButton1Pressed;
        PlayerController.OnInteractionButton2Pressed += OnInteractionButton2Pressed;

        _dataHandlerComponent = GameObject.FindWithTag("DataHandler").GetComponent<DataHandlerComponent>();
    }

    protected virtual void OnInteractionButton1Pressed()
    {
        _interactionButton1Pressed = true;
    }

    protected virtual void OnInteractionButton2Pressed()
    {
        _interactionButton2Pressed = true;
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
        _isCollidingWithPlayer = other.gameObject.layer.Equals(LayerMask.NameToLayer("Player"));
        _interaction1Enabled = _isCollidingWithPlayer && _interactionButton1Holding;
        _interaction2Enabled = _isCollidingWithPlayer && _interactionButton2Pressed;
    }

    protected virtual void OnDestroy()
    {
        PlayerController.OnInteractionButton1Hold -= OnInteractionButton1Hold;
        PlayerController.OnInteractionButton1Released -= OnInteractionButton1Released;
        PlayerController.OnInteractionButton1Pressed -= OnInteractionButton1Pressed;
    }
}