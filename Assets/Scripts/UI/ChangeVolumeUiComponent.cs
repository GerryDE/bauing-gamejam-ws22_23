using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ChangeVolumeUiComponent : MonoBehaviour
{
    private Slider _slider;
    
    void Start()
    {
        _slider = GetComponent<Slider>();

        DataProvider.OnVolumeDataChanged += OnVolumeDataChanged;
        ChangeVolumeMenuComponent.OnVolumeChanged += OnVolumeChanged;
    }

    private void OnVolumeChanged(float volume)
    {
        _slider.value = Mathf.Clamp(volume, 0f, 1f);
    }

    private void OnVolumeDataChanged(AudioVolumeData volumeData)
    {
        _slider.value = volumeData.volume;
    }

    private void OnDestroy() 
    {
        DataProvider.OnVolumeDataChanged -= OnVolumeDataChanged;
        ChangeVolumeMenuComponent.OnVolumeChanged -= OnVolumeChanged;
    }
}
