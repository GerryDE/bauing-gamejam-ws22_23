using UnityEngine;

public class StoneComponent : InteractableBaseComponent
{
    [SerializeField] private int startingMiningDuration = 60;
    [SerializeField] private float miningDurationMultiplicator = 1.2f;

    private int _elapsedMiningTime;
    private int _miningDuration;
    private int _minedStonesCount;

    public delegate void StoneDrop(int amount);

    public static StoneDrop OnStoneDrop;

    protected override void Start()
    {
        base.Start();
        _miningDuration = CalculateMiningDuration();
    }

    private void Update()
    {
        if (!_interaction1Enabled) return;

        _elapsedMiningTime++;
        if (_elapsedMiningTime <= _miningDuration) return;

        OnStoneDrop?.Invoke(1);
        _elapsedMiningTime = 0;
        _minedStonesCount++;
        _miningDuration = CalculateMiningDuration();
    }

    private int CalculateMiningDuration()
    {
        return (int)(startingMiningDuration * Mathf.Pow(miningDurationMultiplicator, _minedStonesCount));
    }
}