using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AfterEffects : MonoBehaviour
{
    Vignette vignette;
    ColorAdjustments colorAdjustments;
    VolumeProfile volumeProfile;
    private void Awake()
    {
        volumeProfile = GetComponent<Volume>()?.profile;
        if (!volumeProfile) throw new System.NullReferenceException(nameof(VolumeProfile));

        if (!volumeProfile.TryGet(out vignette)) throw new System.NullReferenceException(nameof(vignette));
        if (!volumeProfile.TryGet(out colorAdjustments)) throw new System.NullReferenceException(nameof(colorAdjustments));
    }

    public void UpdateVignette(float value)
    {
        vignette.intensity.Override(value);
    }

    public void UpdateSaturaion(float value)
    {
        float normVal = Normalize(value, 0f, 50f, -100f, 0f);
        colorAdjustments.saturation.Override(normVal);
    }

    float Normalize(float val, float valmin, float valmax, float min, float max)
    {
        return (((val - valmin) / (valmax - valmin)) * (max - min)) + min;
    }
}
