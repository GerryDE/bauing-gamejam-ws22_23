using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ChangeVolumeUiComponent : MonoBehaviour
{
    [SerializeField] private AudioVolumeData volumeData;

    private Slider _slider;
    
    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = volumeData.Volume;

        _slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });

        AudioVolumeData.OnVolumeChanged += OnVolumeChanged;
        MenuInputHandlerComponent.OnVolumeChangeTriggered += OnVolumeChangeTriggered;
        GameInputHandlerComponent.OnVolumeChangeCalled += OnVolumeChangeTriggered;
    }

    private void OnVolumeChangeTriggered(float value)
    {
        volumeData.Volume += value * 0.1f;
    }

    private void OnSliderValueChanged()
    {
        volumeData.Volume = _slider.value;
    }

    private void OnVolumeChanged(float volume)
    {
        _slider.value = volume;
    }

    private void OnDestroy() 
    {
        AudioVolumeData.OnVolumeChanged -= OnVolumeChanged;
        MenuInputHandlerComponent.OnVolumeChangeTriggered -= OnVolumeChangeTriggered;
        GameInputHandlerComponent.OnVolumeChangeCalled -= OnVolumeChangeTriggered;
    }
}
