using UnityEngine;

public class YoungToOldTransitionComponent : MonoBehaviour
{
    public delegate void YoungOldTransitionChanged(float newValue);

    public static YoungOldTransitionChanged OnYoungOldTransitionChanged;

    private void Awake()
    {
        DataProvider.OnCurrentRemainingYearsChanged += OnRemainingYearsChanged;
    }

    private void OnRemainingYearsChanged(int remainingYears)
    {
        DataProvider.CurrentPlayerData playerData = DataProvider.Instance.PlayerData;
        if (playerData == null) 
        {
            return;
        }
        
        float transitionValue;
        if (remainingYears > playerData.RemainingYearsForStayingYoung)
        {
            transitionValue = 1f;
        }
        else if (remainingYears > playerData.RemainingYearsUntilBecomingOld)
        {
            transitionValue = (remainingYears - playerData.RemainingYearsUntilBecomingOld) /
                              (float)(playerData.RemainingYearsForStayingYoung -
                                      playerData.RemainingYearsUntilBecomingOld);
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