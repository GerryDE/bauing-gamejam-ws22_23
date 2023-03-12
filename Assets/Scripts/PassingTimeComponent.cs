using UnityEngine;

public class PassingTimeComponent : MonoBehaviour
{
    [SerializeField] private int yearPassedDuration;
    private int _elapsedTime;

    public delegate void YearPassed();

    public static YearPassed OnYearPassed;

    private void Update()
    {
        _elapsedTime++;
        if (_elapsedTime <= yearPassedDuration) return;

        OnYearPassed?.Invoke();
        _elapsedTime = 0;
    }
}