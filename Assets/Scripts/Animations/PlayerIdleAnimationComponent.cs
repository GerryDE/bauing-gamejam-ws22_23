using System;
using UnityEngine;

namespace Animations
{
    [RequireComponent(typeof(Animator))]
    public class PlayerIdleAnimationComponent : MonoBehaviour
    {
        private Animator _animator;

        private void OnEnable()
        {
            PlayerController.OnPlayerMove += OnMove;

            _animator = GetComponent<Animator>();
        }

        private void OnMove(float direction, float velocity)
        {
            _animator.SetBool("isMoving", !velocity.Equals(0f));
            _animator.SetFloat("direction", direction);
        }

        private void OnDisable()
        {
            PlayerController.OnPlayerMove -= OnMove;
        }
    }
}