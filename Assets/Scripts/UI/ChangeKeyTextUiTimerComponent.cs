using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeKeyTextUiTimerComponent : MonoBehaviour
{
    [SerializeField] private float displayDuration = 2f;

    private float _elapsedTime;

    private void FixedUpdate()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > displayDuration)
        {
            _elapsedTime = 0f;
            DataProvider.Instance.UseAlternateLayout = !DataProvider.Instance.UseAlternateLayout;
        }
    }
}
