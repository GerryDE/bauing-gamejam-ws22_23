using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ChangeProgressBarColorComponent : MonoBehaviour
{
    [Range(0f, 1f)] [SerializeField] private float threshold = 0.25f;
    [SerializeField] private Color highHpColor;
    [SerializeField] private Color lowHpColor;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _renderer.color = transform.localScale.x > threshold ? highHpColor : lowHpColor;
    }
}