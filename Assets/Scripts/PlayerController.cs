using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;

    private Rigidbody2D _rigidbody;
    private float _velocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_velocity * (Time.deltaTime * moveSpeed), 0f);
    }

    public void OnMove(InputValue value)
    {
        _velocity = value.Get<float>();
    }
}