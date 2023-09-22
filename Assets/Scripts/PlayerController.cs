using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float _velocity;
    private float _moveSpeedMultiplier = 1f;
    private float _direction;

    public delegate void PlayerMove(float direction, float velocity);

    public static PlayerMove OnPlayerMove;

    private void Awake()
    {
        YoungToOldTransitionComponent.OnYoungOldTransitionChanged += OnYoungOldTransitionChanged;
        GameInputHandlerComponent.OnMoveCalled += OnMoveCalled;

        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnMoveCalled(float velocity)
    {
        _velocity = velocity;
        if (System.Math.Abs(_velocity) > 0.01f)
        {
            _direction = _velocity;
        }
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy"))) return;
        _rigidbody.AddForce(DataProvider.Instance.PlayerData.ThrowForce);
    }

    private void OnDestroy()
    {
        YoungToOldTransitionComponent.OnYoungOldTransitionChanged -= OnYoungOldTransitionChanged;
        GameInputHandlerComponent.OnMoveCalled -= OnMoveCalled;
    }
}