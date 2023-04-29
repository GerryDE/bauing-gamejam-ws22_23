using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float _velocity;
    private float _moveSpeedMultiplier = 1f;
    private float _direction;

    public delegate void InteractionButton1Hold();

    public delegate void InteractionButton1Released();

    public delegate void InteractionButton1Pressed();

    public delegate void InteractionButton2Pressed();

    public delegate void RestartGame();

    public delegate void PlayerMove(float direction, float velocity);

    public static InteractionButton1Hold OnInteractionButton1Hold;
    public static InteractionButton1Released OnInteractionButton1Released;
    public static InteractionButton1Pressed OnInteractionButton1Pressed;
    public static InteractionButton2Pressed OnInteractionButton2Pressed;
    public static PlayerMove OnPlayerMove;
    public static RestartGame OnRestartGame;

    private void Awake()
    {
        YoungToOldTransitionComponent.OnYoungOldTransitionChanged += OnYoungOldTransitionChanged;

        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnYoungOldTransitionChanged(float newValue)
    {
        var minSpeedPercentage = DataProvider.Instance.PlayerData.MinSpeedPercentage;
        _moveSpeedMultiplier = (1 - minSpeedPercentage) * newValue + minSpeedPercentage;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity =
            new Vector2(
                _velocity * _moveSpeedMultiplier * (Time.deltaTime * DataProvider.Instance.PlayerData.MoveSpeed), 0f);
        OnPlayerMove?.Invoke(_direction, _velocity);
    }

    public void OnMove(InputValue value)
    {
        _velocity = value.Get<float>();
        if (_velocity != 0f)
        {
            _direction = _velocity;
        }
    }

    public void OnInteract1Hold(InputValue value)
    {
        var floatValue = value.Get<float>();
        if (floatValue > 0f)
        {
            OnInteractionButton1Hold?.Invoke();
        }
        else
        {
            OnInteractionButton1Released?.Invoke();
        }
    }

    public void OnInteract1Press(InputValue value)
    {
        var floatValue = value.Get<float>();
        if (floatValue > 0f)
        {
            OnInteractionButton1Pressed?.Invoke();
        }
    }

    public void OnInteract2Press(InputValue value)
    {
        var floatValue = value.Get<float>();
        if (floatValue > 0f)
        {
            OnInteractionButton2Pressed?.Invoke();
        }
    }

    public void OnRestart(InputValue value)
    {
        OnRestartGame?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy"))) return;
        _rigidbody.AddForce(DataProvider.Instance.PlayerData.ThrowBackForce);
    }

    private void OnDestroy()
    {
        YoungToOldTransitionComponent.OnYoungOldTransitionChanged -= OnYoungOldTransitionChanged;
    }
}