using UnityEngine;

public class YoungToOldTransitionComponent : MonoBehaviour
{
    public delegate void YoungOldTransitionChanged(float newValue);

    public static YoungOldTransitionChanged OnYoungOldTransitionChanged;

    private static DataProvider.CurrentPlayerData _playerData;

    private void Awake()
    {
        _playerData = DataProvider.Instance.PlayerData;
        DataProvider.OnCurrentRemainingYearsChanged += OnRemainingYearsChanged;
    }

    private void OnRemainingYearsChanged(int remainingYears)
    {
        float transitionValue;
        if (remainingYears > _playerData.RemainingYearsForStayingYoung)
        {
            transitionValue = 1f;
        }
        else if (remainingYears > _playerData.RemainingYearsUntilBecomingOld)
        {
            transitionValue = (remainingYears - _playerData.RemainingYearsUntilBecomingOld) /
                              (float)(_playerData.RemainingYearsForStayingYoung -
                                      _playerData.RemainingYearsUntilBecomingOld);
        }
        else
        {
            transitionValue = 0f;
        }

        OnYoungOldTransitionChanged?.Invoke(transitionValue);
    }

    private void OnDestroy()
    {
        DataProvider.OnCurrentRemainingYearsChanged -= OnRemainingYearsChanged;
    }
}