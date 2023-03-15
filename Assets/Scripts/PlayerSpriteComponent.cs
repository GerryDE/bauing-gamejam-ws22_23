using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerSpriteComponent : MonoBehaviour
{
    [SerializeField] private SpriteRenderer oldSpriteRenderer;

    private SpriteRenderer _renderer;

    private void Start()
    {
        YoungToOldTransitionComponent.OnYoungOldTransitionChanged += OnYoungOldTransitionChanged;

        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnYoungOldTransitionChanged(float newValue)
    {
        _renderer.color = new Color(1f, 1f, 1f, newValue);
        oldSpriteRenderer.color = new Color(1f, 1f, 1f, 1f - newValue);
    }

    private void OnDestroy()
    {
        YoungToOldTransitionComponent.OnYoungOldTransitionChanged -= OnYoungOldTransitionChanged;
    }
}