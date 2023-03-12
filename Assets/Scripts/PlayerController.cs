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
    private bool _isInteracting;

    public delegate void OnPlayerFenceInteraction();

    public delegate void PlayerMove(float xVelocity);

    public delegate void PlayerMiningWoodStart(int instanceId);

    public delegate void PlayerMiningWoodStop(int instanceId);

    public delegate void PlayerMiningStoneStart(int instanceId);

    public delegate void PlayerMiningStoneStop(int instanceId);

    public delegate void PlayerPrayingStatueStart(int instanceId);

    public delegate void PlayerPrayingStatueStop(int instanceId);

    public static OnPlayerFenceInteraction onPlayerFenceInteraction;
    public static PlayerMove OnPlayerMove;
    public static PlayerMiningWoodStart OnPlayerMiningWoodStart;
    public static PlayerMiningWoodStop OnPlayerMiningWoodStop;
    public static PlayerMiningStoneStart OnPlayerMiningStoneStart;
    public static PlayerMiningStoneStop OnPlayerMiningStoneStop;
    public static PlayerPrayingStatueStart OnPlayerPrayingStatueStart;
    public static PlayerPrayingStatueStop OnPlayerPrayingStatueStop;

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

    public void OnInteract(InputValue value)
    {
        _isInteracting = value.Get<float>() > 0f;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Tree")) && _isInteracting)
        {
            OnPlayerMiningWoodStart?.Invoke(other.gameObject.GetInstanceID());
        }
        else
        {
            OnPlayerMiningWoodStop?.Invoke(other.gameObject.GetInstanceID());
        }

        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Stone")) && _isInteracting)
        {
            OnPlayerMiningStoneStart?.Invoke(other.gameObject.GetInstanceID());
        }
        else
        {
            OnPlayerMiningStoneStop?.Invoke(other.gameObject.GetInstanceID());
        }

        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Statue")) && _isInteracting)
        {
            OnPlayerPrayingStatueStart?.Invoke(other.gameObject.GetInstanceID());
        }
        else
        {
            OnPlayerPrayingStatueStop?.Invoke(other.gameObject.GetInstanceID());
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy"))) return;
        _rigidbody.AddForce(throwBackForce);
    }
}