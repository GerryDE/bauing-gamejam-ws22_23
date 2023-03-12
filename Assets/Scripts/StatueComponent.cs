using UnityEngine;

public class StatueComponent : InteractableBaseComponent
{
    [SerializeField] private int startingPrayingDuration = 60;
    [SerializeField] private float prayingDurationMultiplicator = 0.9f;

    private int _elapsedTime;
    private int _prayingDuration;
    private int _prayCount;

    public delegate void Prayed(int amount);

    public static Prayed OnPrayed;

    protected override void Start()
    {
        base.Start();
        _prayingDuration = CalculatePrayingDuration();
    }

    private void Update()
    {
        if (!_interaction1Enabled)
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

    private int CalculatePrayingDuration()
    {
        return (int)(startingPrayingDuration * Mathf.Pow(prayingDurationMultiplicator, _prayCount));
    }
}