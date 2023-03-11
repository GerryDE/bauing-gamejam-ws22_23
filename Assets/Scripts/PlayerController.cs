using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 300f;
    public int coolDownTime = 60;

    private Rigidbody2D _rigidbody;
    private float _velocity;
    private bool _isCollidingWithFence;
    private bool _isInteracting;
    private int _currentCooldown;

    public delegate void OnPlayerFenceInteraction();

    public static OnPlayerFenceInteraction onPlayerFenceInteraction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_velocity * (Time.deltaTime * moveSpeed), 0f);
    }

    private void Update()
    {
        if (_isInteracting)
        {
            if (_isCollidingWithFence)
            {
                _currentCooldown++;
                if (_currentCooldown >= coolDownTime)
                {
                    onPlayerFenceInteraction?.Invoke();
                    _currentCooldown = 0;
                }
            }
        }
        else
        {
            _currentCooldown = 0;
        }
    }

    public void OnMove(InputValue value)
    {
        _velocity = value.Get<float>();
    }

    public void OnInteract(InputValue value)
    {
        _isInteracting = value.Get<float>() > 0f;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Fence")))
        {
            _isCollidingWithFence = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Fence")))
        {
            _isCollidingWithFence = false;
        }
    }
}