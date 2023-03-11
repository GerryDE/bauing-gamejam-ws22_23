using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public int maxHp = 100;
    public float moveSpeed = 300f;
    public int coolDownTime = 60;
    public Vector2 throwBackForce = new Vector2(100f, 0f);
    public bool isFacingLeft = true;

    private Rigidbody2D _rigidbody;
    private float _velocity;
    private bool _isCollidingWithFence;
    private bool _isInteracting;
    [SerializeField] private int _currentHp;
    private int _currentCooldown;

    public delegate void OnPlayerFenceInteraction();

    public static OnPlayerFenceInteraction onPlayerFenceInteraction;

    public delegate void PlayerMove(float xVelocity);

    public static PlayerMove OnPlayerMove;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _currentHp = maxHp;
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
        if (col.gameObject.layer.Equals(LayerMask.NameToLayer("FenceTrigger")))
        {
            _isCollidingWithFence = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("FenceTrigger")))
        {
            _isCollidingWithFence = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")))
        {
            _currentHp--;

            if (_currentHp <= 0)
            {
                Debug.Log("GAME OVER!");
                return;
            }

            _rigidbody.AddForce(new Vector2(throwBackForce.x * (isFacingLeft ? 1f : -1f), throwBackForce.y));
        }
    }
}