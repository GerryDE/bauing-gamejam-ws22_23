using System;
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
    private bool _isCollidingWithTree;
    private bool _isCollidingWithStoneQuarry;
    private bool _isCollidingWithStatue;
    private bool _isInteracting;
    [SerializeField] private int currentHp;
    private int _currentCooldown;

    [SerializeField] private int woodCount;
    [SerializeField] private int stoneCount;

    [Header("Age handling")] [SerializeField]
    private int minAge = 20;

    [SerializeField] private int currentAge;
    [SerializeField] private int maxAge = 100;
    [SerializeField] private int yearDuration = 60;
    [SerializeField] private int statueHealAmount = 20;
    [SerializeField] private int statueInteractionCooldown = 300;

    private int _yearPassedCounter;

    public delegate void OnPlayerFenceInteraction();

    public delegate void PlayerMove(float xVelocity);

    public delegate void PlayerTreeInteraction(int woodCount);

    public delegate void PlayerStoneQuarryInteraction(int stoneCount);

    public delegate void PlayerStatueInteraction(int age);

    public static OnPlayerFenceInteraction onPlayerFenceInteraction;
    public static PlayerMove OnPlayerMove;
    public static PlayerTreeInteraction OnPlayerTreeInteraction;
    public static PlayerStoneQuarryInteraction OnPlayerStoneQuarryInteraction;
    public static PlayerStatueInteraction OnPlayerAgeChanged;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        currentHp = maxHp;
        currentAge = minAge;

        OnPlayerAgeChanged?.Invoke(currentAge);
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
        // Handle age
        _yearPassedCounter++;
        if (_yearPassedCounter > yearDuration)
        {
            currentAge++;
            OnPlayerAgeChanged?.Invoke(currentAge);

            if (currentAge > maxAge)
            {
                // TODO Handle Game Over
                Debug.Log("GAME OVER!");
            }

            _yearPassedCounter = 0;
        }

        // Handle interactions with objects
        if (_isCollidingWithStatue)
        {
            _currentCooldown++;
            if (_currentCooldown > statueInteractionCooldown)
            {
                DecreaseAge();
                OnPlayerAgeChanged?.Invoke(currentAge);
                _currentCooldown = 0;
            }
        }
        else if (_isCollidingWithFence)
        {
            _currentCooldown++;
            if (_currentCooldown > coolDownTime)
            {
                onPlayerFenceInteraction?.Invoke();
                _currentCooldown = 0;
            }
        }
        else if (_isCollidingWithTree)
        {
            _currentCooldown++;
            if (_currentCooldown > coolDownTime)
            {
                woodCount++;
                OnPlayerTreeInteraction?.Invoke(woodCount);
                _currentCooldown = 0;
            }
        }
        else if (_isCollidingWithStoneQuarry)
        {
            _currentCooldown++;
            if (_currentCooldown > coolDownTime)
            {
                stoneCount++;
                OnPlayerStoneQuarryInteraction?.Invoke(stoneCount);
                _currentCooldown = 0;
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

    private void DecreaseAge()
    {
        currentAge = Math.Max(minAge, currentAge - statueHealAmount);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer.Equals(LayerMask.NameToLayer("FenceTrigger")))
        {
            _isCollidingWithFence = true;
        }

        if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Tree")))
        {
            _isCollidingWithTree = true;
        }

        if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Stone")))
        {
            _isCollidingWithStoneQuarry = true;
        }

        if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Statue")))
        {
            _isCollidingWithStatue = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("FenceTrigger")))
        {
            _isCollidingWithFence = false;
        }

        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Tree")))
        {
            _isCollidingWithTree = false;
        }

        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Stone")))
        {
            _isCollidingWithStoneQuarry = false;
        }

        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Statue")))
        {
            _isCollidingWithStatue = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")))
        {
            currentHp--;

            if (currentHp <= 0)
            {
                Debug.Log("GAME OVER!");
                return;
            }

            _rigidbody.AddForce(throwBackForce);
        }
    }
}