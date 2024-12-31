using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.AI;
using Data;
using Data.objective;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FadeInAndOutTextComponent : MonoBehaviour
{
    [SerializeField] private float fadeInDuration;
    [SerializeField] private float fadeOutDuration;
    [SerializeField] private float displayDuration;
    [SerializeField] private SkipTutorialData skipTutorialData;

    private TextMeshProUGUI _textComponent;
    private float _elapsedTime;

    // Start is called before the first frame update
    void OnEnable()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();
        _textComponent.enabled = false;
        TutorialComponent.OnTutorialCompleted += OnTutorialCompleted;
        TutorialComponent.OnNewObjectiveStarted += OnNewObjectiveStarted;
    }

    private void OnNewObjectiveStarted(ObjectiveData data)
    {
        if (data.GetType() == typeof(TutorialCompletedObjectiveData))
            _textComponent.enabled = true;
            _textComponent.alpha = 0f;
            _elapsedTime = 0f;
    }

    private void OnTutorialCompleted()
    {
        if (skipTutorialData.ShallBeSkipped) {
            return;
        }

        _textComponent.enabled = true;
        _textComponent.alpha = 0f;
        _elapsedTime = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_textComponent.enabled) return;
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime < fadeInDuration)
        {
            _textComponent.alpha = _elapsedTime / fadeInDuration;
        } 
        else if (_elapsedTime < fadeInDuration + displayDuration)
        {
            _textComponent.alpha = 1f;
        } 
        else if (_elapsedTime < fadeInDuration + displayDuration + fadeOutDuration)
        {
            _textComponent.alpha = 1f - ((_elapsedTime - fadeInDuration - displayDuration) / fadeOutDuration);
        } 
        else 
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable() {
        TutorialComponent.OnTutorialCompleted -= OnTutorialCompleted;
        TutorialComponent.OnNewObjectiveStarted -= OnNewObjectiveStarted;
    }
}
