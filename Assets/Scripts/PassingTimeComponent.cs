using UnityEngine;

public class PassingTimeComponent : MonoBehaviour
{
    [SerializeField] private float yearPassedDuration = 2f;
    private float _elapsedTime;

    public delegate void YearPassed();

    public static YearPassed OnYearPassed;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime <= yearPassedDuration) return;

        OnYearPassed?.Invoke();
        _elapsedTime = 0f;
    }
}