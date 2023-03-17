using System;
using UnityEngine;

public class ProgressBarComponent : MonoBehaviour
{
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private SpriteRenderer foreground;
    private float _currentTime;
    private float _requiredTime;

    private void Start()
    {
        Disable();
    }

    public void UpdateValues(float currentTime, float requiredTime)
    {
        _currentTime = currentTime;
        _requiredTime = requiredTime;
        
        foreground.gameObject.transform.localScale = new Vector3(Mathf.Min(currentTime / requiredTime, 1f), foreground.gameObject.transform.localScale.y, 0f);
    }

    public void Enable()
    {
        background.enabled = true;
        foreground.enabled = true;
    }

    public void Disable()
    {
        background.enabled = false;
        foreground.enabled = false;
    }
}
