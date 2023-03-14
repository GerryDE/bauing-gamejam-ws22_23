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

        _renderer = GetComponent<SpriteRenderer>();
        _dataHandlerComponent = GameObject.FindWithTag("DataHandler").GetComponent<DataHandlerComponent>();
    }

    private void Update()
    {
        var remainingYears = _dataHandlerComponent.RemainingYears;
        if (remainingYears > remainingYearsForYoung)
        {
            _renderer.color = new Color(1f, 1f, 1f, 1f);
            oldSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        }
        else if (remainingYears > remainingYearsForOld)
        {
            var alpha = (remainingYears - remainingYearsForOld) /
                        (float)(remainingYearsForYoung - remainingYearsForOld);
            _renderer.color = new Color(1f, 1f, 1f, alpha);
            oldSpriteRenderer.color = new Color(1f, 1f, 1f, 1f - alpha);
        }
        else
        {
            _renderer.color = new Color(1f, 1f, 1f, 0f);
            oldSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
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