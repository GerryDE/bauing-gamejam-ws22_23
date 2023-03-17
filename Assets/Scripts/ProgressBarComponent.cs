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
        
        foreground.gameObject.transform.localScale = Vector3.right * (currentTime / requiredTime);
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
