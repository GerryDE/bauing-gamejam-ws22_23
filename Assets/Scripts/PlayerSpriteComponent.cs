using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerSpriteComponent : MonoBehaviour
{
    [SerializeField] private int remainingYearsForYoung;
    [SerializeField] private int remainingYearsForOld;
    [SerializeField] private SpriteRenderer oldSpriteRenderer;

    private SpriteRenderer _renderer;
    private DataHandlerComponent _dataHandlerComponent;

    private void Start()
    {
        PlayerController.OnPlayerMove += HandleHorizontalFlip;
        YoungToOldTransitionComponent.OnYoungOldTransitionChanged += OnYoungOldTransitionChanged;

        _renderer = GetComponent<SpriteRenderer>();
        _dataHandlerComponent = GameObject.FindWithTag("DataHandler").GetComponent<DataHandlerComponent>();
    }

    private void OnYoungOldTransitionChanged(float newValue)
    {
        _renderer.color = new Color(1f, 1f, 1f, newValue);
        oldSpriteRenderer.color = new Color(1f, 1f, 1f, 1f - newValue);
    }

    private void HandleHorizontalFlip(float xVelocity)
    {
        if (xVelocity < 0f)
        {
            _renderer.flipX = false;
        }

        if (xVelocity > 0f)
        {
            _renderer.flipX = true;
        }
    }
}