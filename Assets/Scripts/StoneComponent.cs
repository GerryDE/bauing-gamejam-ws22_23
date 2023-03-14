using UnityEngine;

public class StoneComponent : InteractableBaseComponent
{
    [SerializeField] private float startingMiningDuration = 1f;
    [SerializeField] private float miningDurationMultiplicator = 1f;

    private float _elapsedMiningTime;
    private float _miningDuration;
    private int _minedStonesCount;

    public delegate void StoneDrop(int amount);

    public static StoneDrop OnStoneDrop;

    protected override void Start()
    {
        base.Start();
        _miningDuration = CalculateMiningDuration();
    }

    private void FixedUpdate()
    {
        if (!_interaction1Enabled) return;

        _elapsedMiningTime += Time.deltaTime;
        if (_elapsedMiningTime <= _miningDuration) return;

        OnStoneDrop?.Invoke(1);
        _elapsedMiningTime = 0f;
        _minedStonesCount++;
        _miningDuration = CalculateMiningDuration();
    }

    private int CalculateMiningDuration()
    {
        return (int)(startingMiningDuration * Mathf.Pow(miningDurationMultiplicator, _minedStonesCount));
    }
}