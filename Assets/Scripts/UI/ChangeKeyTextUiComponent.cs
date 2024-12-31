using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeKeyTextUiComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyText1;
    [SerializeField] private TextMeshProUGUI keyText2;
    [SerializeField] private float transitionSpeed = 1f;

    private TextMeshProUGUI _currentKeyText;
    private TextMeshProUGUI _nextKeyText;

    private void Start() 
    {
        OnUseAlternateLayoutChanged(DataProvider.Instance.UseAlternateLayout);
    }

    private void OnEnable() {
        bool useAlternateLayout = DataProvider.Instance != null && DataProvider.Instance.UseAlternateLayout;
        OnUseAlternateLayoutChanged(useAlternateLayout);
        Color currentKeyColor = _currentKeyText.color;
        _currentKeyText.color = new Color(currentKeyColor.r, currentKeyColor.g, currentKeyColor.b, 1f);

        Color nextKeyColor = _nextKeyText.color;
        _nextKeyText.color = new Color(nextKeyColor.r, nextKeyColor.g, nextKeyColor.b, 0f);

        DataProvider.OnUseAlternateLayoutChanged += OnUseAlternateLayoutChanged;
    }

    private void FixedUpdate()
    {
        Color currentKeyColor = _currentKeyText.color;
        _currentKeyText.color = new Color(currentKeyColor.r, currentKeyColor.g, currentKeyColor.b, Mathf.Min(1f, currentKeyColor.a + transitionSpeed * Time.deltaTime));

        Color nextKeyColor = _nextKeyText.color;
        _nextKeyText.color = new Color(nextKeyColor.r, nextKeyColor.g, nextKeyColor.b, Mathf.Max(0f, nextKeyColor.a - transitionSpeed * Time.deltaTime));
    }

    private void OnUseAlternateLayoutChanged(bool useAlternateLayout)
    {
        if (!useAlternateLayout)
        {
            _currentKeyText = keyText1;
            _nextKeyText = keyText2;
        } else 
        {
            _currentKeyText = keyText2;
            _nextKeyText = keyText1;
        }
    }

    private void OnDisable() {
        DataProvider.OnUseAlternateLayoutChanged -= OnUseAlternateLayoutChanged;
    }
}
