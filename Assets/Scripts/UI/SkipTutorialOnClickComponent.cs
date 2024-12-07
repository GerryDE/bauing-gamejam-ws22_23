using System.Collections;
using System.Collections.Generic;
using Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class SkipTutorialOnClickComponent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SkipTutorialData data;

    private Toggle _toggle;

    public void Start() {
        _toggle = GetComponent<Toggle>();

        data.ShallBeSkipped = false;
        _toggle.isOn = data.ShallBeSkipped;

        MenuInputHandlerComponent.OnSkipTutorialButtonTriggered += OnSkipTutorialButtonTriggered;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        OnSkipTutorialButtonTriggered();
    }

    public void OnSkipTutorialButtonTriggered()
    {
        data.ShallBeSkipped = !data.ShallBeSkipped;
        _toggle.isOn = data.ShallBeSkipped;
    }
}
