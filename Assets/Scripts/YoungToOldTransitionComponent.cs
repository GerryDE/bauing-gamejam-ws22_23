using Data;
using UnityEngine;

public class YoungToOldTransitionComponent : MonoBehaviour
{
    public delegate void YoungOldTransitionChanged(float newValue);

    public static YoungOldTransitionChanged OnYoungOldTransitionChanged;

    private static PlayerData _playerData;

    private void Awake()
    {
        _playerData = DataProvider.Instance.PlayerData;
        PlayerData.OnPlayerCurrentRemainingYearsChanged += OnRemainingYearsChanged;
    }

    private void OnRemainingYearsChanged(int remainingYears)
    {
        float transitionValue;
        if (remainingYears > _playerData.RemainingYearsForStayingYoung)
        {
            transitionValue = 1f;
        }
        else if (remainingYears > _playerData.RemainingYearsForBecomingOld)
        {
            transitionValue = (remainingYears - _playerData.RemainingYearsForBecomingOld) /
                              (float)(_playerData.RemainingYearsForStayingYoung -
                                      _playerData.RemainingYearsForBecomingOld);
        }
        else
        {
            transitionValue = 0f;
        }

        OnYoungOldTransitionChanged?.Invoke(transitionValue);
    }
}