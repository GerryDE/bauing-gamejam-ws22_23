using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeKeyTextUiComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyText1;
    [SerializeField] private TextMeshProUGUI keyText2;
    [SerializeField] private float displayDuration;
    [SerializeField] private float transitionSpeed;

    private float _elapsedTime;
    private TextMeshProUGUI _currentKeyText;
    private TextMeshProUGUI _nextKeyText;

    private void Start() 
    {
        _currentKeyText = keyText1;
        _nextKeyText = keyText2;
        Color nextKeyColor = _nextKeyText.color;
        _nextKeyText.color = new Color(nextKeyColor.r, nextKeyColor.g, nextKeyColor.b, 0f);
    }

    private void FixedUpdate()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > displayDuration)
        {
            _elapsedTime = 0f;
            (_nextKeyText, _currentKeyText) = (_currentKeyText, _nextKeyText);
        }

        Color currentKeyColor = _currentKeyText.color;
        _currentKeyText.color = new Color(currentKeyColor.r, currentKeyColor.g, currentKeyColor.b, Mathf.Min(1f, currentKeyColor.a + transitionSpeed * Time.deltaTime));

        Color nextKeyColor = _nextKeyText.color;
        _nextKeyText.color = new Color(nextKeyColor.r, nextKeyColor.g, nextKeyColor.b, Mathf.Max(0f, nextKeyColor.a - transitionSpeed * Time.deltaTime));
    }
}
