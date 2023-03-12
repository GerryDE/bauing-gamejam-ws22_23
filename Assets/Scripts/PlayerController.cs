using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 300f;
    public Vector2 throwBackForce = new Vector2(100f, 0f);
    public bool isFacingLeft = true;

    private Rigidbody2D _rigidbody;
    private float _velocity;
    private bool _isCollidingWithFence;

    public delegate void InteractionButton1Hold();

    public delegate void InteractionButton1Released();

    public delegate void InteractionButton1Pressed();

    public delegate void PlayerMove(float xVelocity);

    public static InteractionButton1Hold OnInteractionButton1Hold;
    public static InteractionButton1Released OnInteractionButton1Released;
    public static InteractionButton1Pressed OnInteractionButton1Pressed;
    public static PlayerMove OnPlayerMove;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_velocity * (Time.deltaTime * moveSpeed), 0f);
        if (_velocity != 0f)
        {
            OnPlayerMove?.Invoke(_velocity);
            if (_velocity < 0f)
            {
                isFacingLeft = true;
            }
            else
            {
                isFacingLeft = false;
            }
        }
    }

    public void OnMove(InputValue value)
    {
        _velocity = value.Get<float>();
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy"))) return;
        _rigidbody.AddForce(throwBackForce);
    }
}