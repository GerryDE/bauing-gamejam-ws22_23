using System;
using Data.objective;
using UnityEngine;

public abstract class InteractableBaseComponent : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer upgradeNotificationSprite;
    protected DataHandlerComponent _dataHandlerComponent;
    protected bool _interactionButton1Holding;
    protected bool _interactionButton1Pressed;
    protected bool _interactionButton2Pressed;
    protected bool _interaction1Enabled;
    protected bool _interaction2Enabled;
    protected bool _isCollidingWithPlayer;
    protected bool _upgradeEnabled = false;

    protected virtual void Start()
    {
        GameInputHandlerComponent.OnInteract1HoldCalled += OnInteractionButton1Hold;
        GameInputHandlerComponent.OnInteract1ReleasedCalled += OnInteractionButton1Released;
        GameInputHandlerComponent.OnInteract1PressCalled += OnInteractionButton1Pressed;
        GameInputHandlerComponent.OnInteract2PressCalled += OnInteractionButton2Pressed;
        PlayerController.OnPlayerMove += OnPlayerMove;
        DataProvider.OnResourceDataChanged += OnResourceDataChanged;
        TutorialComponent.OnNewObjectiveStarted += OnNewObjectiveStarted;

        _dataHandlerComponent = GameObject.FindWithTag("DataHandler").GetComponent<DataHandlerComponent>();
    }

    protected virtual void OnNewObjectiveStarted(ObjectiveData data)
    {
        if (data.GetType() != typeof(UpgradeObjectiveData)) return;
        _upgradeEnabled = true;
    }

    protected virtual void OnPlayerMove(float direction, float velocity)
    {
        if (velocity.Equals(0f)) return;
        _interactionButton1Pressed = false;
        _interaction1Enabled = false;
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

    protected virtual void OnResourceDataChanged(DataProvider.CurrentResourceData resourceData)
    {
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        _isCollidingWithPlayer = other.gameObject.layer.Equals(LayerMask.NameToLayer("Player"));

        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")))
        {
            _interactionButton1Pressed = false;
            _interactionButton2Pressed = false;
        }
        
        _interaction1Enabled = _isCollidingWithPlayer && _interactionButton1Pressed;
        _interaction2Enabled = _isCollidingWithPlayer && _interactionButton2Pressed;
    }

    protected virtual void OnDestroy()
    {
        GameInputHandlerComponent.OnInteract1HoldCalled -= OnInteractionButton1Hold;
        GameInputHandlerComponent.OnInteract1ReleasedCalled -= OnInteractionButton1Released;
        GameInputHandlerComponent.OnInteract1PressCalled -= OnInteractionButton1Pressed;
        GameInputHandlerComponent.OnInteract2PressCalled -= OnInteractionButton2Pressed;
        PlayerController.OnPlayerMove -= OnPlayerMove;
        DataProvider.OnResourceDataChanged -= OnResourceDataChanged;
    }
}