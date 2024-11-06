using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVolumeMenuComponent : MonoBehaviour
{
    [SerializeField] private AudioVolumeData audioVolumeData;

    public delegate void VolumeChanged(float volume);

    public static VolumeChanged OnVolumeChanged;

    void Start()
    {
        MenuInputHandlerComponent.OnVolumeChangeTriggered += OnVolumeChangeTriggered;
    }

    private void OnVolumeChangeTriggered(float value)
    {
        if (value < 0f)
        {
            audioVolumeData.volume -= 0.1f;
            audioVolumeData.volume = Mathf.Clamp(audioVolumeData.volume, 0f, 1f);
            OnVolumeChanged?.Invoke(audioVolumeData.volume);
        }
        else if (value > 0f)
        {
            audioVolumeData.volume += 0.1f;
            audioVolumeData.volume = Mathf.Clamp(audioVolumeData.volume, 0f, 1f);
            OnVolumeChanged?.Invoke(audioVolumeData.volume);
        }
    }

    private void OnDestroy() 
    {
        MenuInputHandlerComponent.OnVolumeChangeTriggered -= OnVolumeChangeTriggered;
    }
}
