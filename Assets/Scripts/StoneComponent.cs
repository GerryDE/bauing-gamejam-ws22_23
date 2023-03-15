using UnityEngine;
using UnityEngine.Serialization;

public class StoneComponent : InteractableBaseComponent
{
    [SerializeField] private float startingMiningDuration = 1f;
    [SerializeField] private float miningDurationMultiplicator = 1f;
    [SerializeField] private SpriteRenderer renderer;

    private float _elapsedMiningTime;
    private float _currentBaseMiningDuration;
    private float _miningDuration;
    private int _minedStonesCount;
    private int _dropAmount = 1;

    public delegate void StoneDrop(int amount);

    public static StoneDrop OnStoneDrop;

    protected override void Start()
    {
        base.Start();
        StoneUpgradeComponent.OnUpgradeMine += OnUpgradeMine;
        _currentBaseMiningDuration = startingMiningDuration;
        _miningDuration = CalculateMiningDuration();
    }

    private void OnUpgradeMine(float newMiningDuration, int newDropAmount, Sprite sprite)
    {
        renderer.sprite = sprite;
        _currentBaseMiningDuration = newMiningDuration;
        _dropAmount = newDropAmount;
    }

    private void FixedUpdate()
    {
        if (!_interaction1Enabled) return;

        _elapsedMiningTime += Time.deltaTime;
        if (_elapsedMiningTime <= _miningDuration) return;

        OnStoneDrop?.Invoke(_dropAmount);
        _elapsedMiningTime = 0f;
        _minedStonesCount += _dropAmount;
        _miningDuration = CalculateMiningDuration();
    }

    private float CalculateMiningDuration()
    {
        return (_currentBaseMiningDuration * Mathf.Pow(miningDurationMultiplicator, _minedStonesCount));
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        StoneUpgradeComponent.OnUpgradeMine -= OnUpgradeMine;
    }
}