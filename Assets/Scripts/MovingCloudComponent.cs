using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovingCloudComponent : MonoBehaviour
    {
        [SerializeField] private float respawnPosition;
        [SerializeField] private float endingPosition;
        [SerializeField] private float moveSpeed;
        [SerializeField] private SpriteRenderer _renderer;
        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            // SetPosition(startingPosition);
        }

        private void SetPosition(float x)
        {
            transform.position = new Vector3(x, transform.position.y, 0f);
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = Vector2.right * (moveSpeed * Time.deltaTime);
        }

        private void Update()
        {
            if (Math.Abs(transform.position.x - endingPosition) < 0.2f)
            {
                SetPosition(respawnPosition);
            }
        }
    }
}