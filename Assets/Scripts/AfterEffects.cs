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
    [SerializeField] float fadeSpeed;
    private float fadeAmount = 0;
    private void Awake()
    {
        volumeProfile = GetComponent<Volume>()?.profile;
        if (!volumeProfile) throw new System.NullReferenceException(nameof(VolumeProfile));

        if (!volumeProfile.TryGet(out vignette)) throw new System.NullReferenceException(nameof(vignette));
        if (!volumeProfile.TryGet(out colorAdjustments)) throw new System.NullReferenceException(nameof(colorAdjustments));
    }

    public void UpdateVignette(float value)
    {
        if(value == 0.0f)
        {
            StartCoroutine(VignetteOnDeath());
        }
    }

    public void UpdateSaturaion(float value)
    {
        float normVal = Normalize(value, 0f, 50f, -100f, 0f);
        colorAdjustments.saturation.Override(Mathf.Min(normVal, 0f));
    }

    float Normalize(float val, float valmin, float valmax, float min, float max)
    {
        return (((val - valmin) / (valmax - valmin)) * (max - min)) + min;
    }

    IEnumerator VignetteOnDeath()
    {
        while (true)
        {
            if(fadeAmount >= 5f)
            {
                while(fadeAmount <=300f)
                {
                    fadeAmount += fadeSpeed * Time.deltaTime * 50;
                    vignette.intensity.Override(fadeAmount);
                    yield return null;
                }
                yield break;
            }
            fadeAmount += fadeSpeed * Time.deltaTime;
            vignette.intensity.Override(fadeAmount);
            yield return null;
        }
    }
}