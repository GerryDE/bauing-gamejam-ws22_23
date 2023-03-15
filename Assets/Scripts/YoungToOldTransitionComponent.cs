using System;
using UnityEngine;

public class YoungToOldTransitionComponent : MonoBehaviour
{
    [SerializeField] private int remainingYearsForYoung;
    [SerializeField] private int remainingYearsForOld;

    private DataHandlerComponent _dataHandlerComponent;

    public delegate void YoungOldTransitionChanged(float newValue);

    public static YoungOldTransitionChanged OnYoungOldTransitionChanged;

    private void Start()
    {
        DataHandlerComponent.OnRemainingYearsChanged += OnRemainingYearsChanged;
    }

    private void OnRemainingYearsChanged(int remainingYears)
    {
        float transitionValue;
        if (remainingYears > remainingYearsForYoung)
        {
            transitionValue = 1f;
        }
        else if (remainingYears > remainingYearsForOld)
        {
            transitionValue = (remainingYears - remainingYearsForOld) /
                              (float)(remainingYearsForYoung - remainingYearsForOld);
        }
        else
        {
            transitionValue = 0f;
        }
        
        OnYoungOldTransitionChanged?.Invoke(transitionValue);
    }

    private void OnDestroy()
    {
        DataHandlerComponent.OnRemainingYearsChanged -= OnRemainingYearsChanged;
    }
}