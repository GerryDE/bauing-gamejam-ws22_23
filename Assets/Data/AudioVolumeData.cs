using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Assets/Data/Audio Volume Data", menuName = "Data/Audio Volume Data", order = 0)]
public class AudioVolumeData : ScriptableObject
{
    public delegate void VolumeChanged(float volume);
    public static VolumeChanged OnVolumeChanged;

    [SerializeField, Range(0f, 1f)] private float _volume = 1f;

    public float Volume
    {
        get => _volume;
        set 
        {
            _volume = Mathf.Clamp(value, 0f, 1f);;
            OnVolumeChanged?.Invoke(value);
        }
    }
}
