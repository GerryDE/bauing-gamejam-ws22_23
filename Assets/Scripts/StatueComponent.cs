using UnityEngine;

public class StatueComponent : MonoBehaviour
{
    [SerializeField] private int startingPrayingDuration = 60;
    [SerializeField] private float prayingDurationMultiplicator = 0.9f;

    private bool _isGettingPrayed;
    private int _elapsedTime;
    private int _prayingDuration;
    private int _prayCount;

    public delegate void Prayed(int amount);

    public static Prayed OnPrayed;

    private void Start()
    {
        PlayerController.OnPlayerPrayingStatueStart += OnPlayerPrayingStatueStart;
        PlayerController.OnPlayerPrayingStatueStop += OnPlayerPrayingStatueStop;

        _prayingDuration = CalculatePrayingDuration();
    }

    private void Update()
    {
        if (!_isGettingPrayed)
        {
            _prayCount = 0;
            return;
        }

        _elapsedTime++;
        if (_elapsedTime <= _prayingDuration) return;

        OnPrayed?.Invoke(1);
        _elapsedTime = 0;
        _prayCount++;
        _prayingDuration = CalculatePrayingDuration();
    }

    private void OnPlayerPrayingStatueStart(int instanceId)
    {
        if (instanceId.Equals(gameObject.GetInstanceID()))
        {
            _isGettingPrayed = true;
        }
    }

    private void OnPlayerPrayingStatueStop(int instanceId)
    {
        if (instanceId.Equals(gameObject.GetInstanceID()))
        {
            _isGettingPrayed = false;
        }
    }

    private void OnDestroy()
    {
        PlayerController.OnPlayerPrayingStatueStart -= OnPlayerPrayingStatueStart;
        PlayerController.OnPlayerPrayingStatueStop -= OnPlayerPrayingStatueStop;
    }

    private int CalculatePrayingDuration()
    {
        return (int)(startingPrayingDuration * Mathf.Pow(prayingDurationMultiplicator, _prayCount));
    }
}