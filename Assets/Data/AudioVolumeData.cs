using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Assets/Data/Audio Volume Data", menuName = "Data/Audio Volume Data", order = 0)]
public class AudioVolumeData : ScriptableObject
{
    [Range(0f, 1f)] public float volume = 1f;
    public bool muted = false;
}
