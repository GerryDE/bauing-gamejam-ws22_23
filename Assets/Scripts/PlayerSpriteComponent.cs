using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerSpriteComponent : MonoBehaviour
{
    private SpriteRenderer _renderer;

    private void Start()
    {
        PlayerController.OnPlayerMove += HandleHorizontalFlip;

        _renderer = GetComponent<SpriteRenderer>();
    }

    private void HandleHorizontalFlip(float xVelocity)
    {
        if (xVelocity < 0f)
        {
            _renderer.flipX = false;
        }

        if (xVelocity > 0f)
        {
            _renderer.flipX = true;
        }
    }
}