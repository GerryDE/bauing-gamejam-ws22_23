using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;

    private Rigidbody2D _rigidbody;
    private float _velocity;
    private bool _isInteracting;

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
        Debug.Log((_isInteracting));
    }

    public void OnMove(InputValue value)
    {
        _velocity = value.Get<float>();
    }

    public void OnInteract(InputValue value)
    {
        _isInteracting = value.Get<float>() > 0f;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        // TODO Add cooldown for Player/Fence interaction
        if (!_isInteracting || !other.gameObject.layer.Equals(LayerMask.NameToLayer("Fence"))) return;
        onPlayerFenceInteraction?.Invoke();
    }
}