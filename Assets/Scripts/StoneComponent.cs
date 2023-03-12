using UnityEngine;

public class StoneComponent : MonoBehaviour
{
    [SerializeField] private int startingMiningDuration = 60;
    [SerializeField] private float miningDurationMultiplicator = 1.2f;

    private bool _isGettingMined;
    private int _elapsedMiningTime;
    private int _miningDuration;
    private int _minedStonesCount;

    public delegate void StoneDrop(int amount);

    public static StoneDrop OnStoneDrop;

    private void Start()
    {
        PlayerController.OnPlayerMiningStoneStart += OnPlayerMiningStoneStart;
        PlayerController.OnPlayerMiningStoneStop += OnPlayerMiningStoneStop;

        _miningDuration = CalculateMiningDuration();
    }

    private void Update()
    {
        if (!_isGettingMined) return;

        _elapsedMiningTime++;
        if (_elapsedMiningTime <= _miningDuration) return;
        
        OnStoneDrop?.Invoke(1);
        _elapsedMiningTime = 0;
        _minedStonesCount++;
        _miningDuration = CalculateMiningDuration();
    }

    private void OnPlayerMiningStoneStart(int instanceId)
    {
        if (instanceId.Equals(gameObject.GetInstanceID()))
        {
            _isGettingMined = true;
        }
    }

    private void OnPlayerMiningStoneStop(int instanceId)
    {
        if (instanceId.Equals(gameObject.GetInstanceID()))
        {
            _isGettingMined = false;
        }
    }

    private void OnDestroy()
    {
        PlayerController.OnPlayerMiningStoneStart -= OnPlayerMiningStoneStart;
        PlayerController.OnPlayerMiningStoneStop -= OnPlayerMiningStoneStop;
    }

    private int CalculateMiningDuration()
    {
        return (int)(startingMiningDuration * Mathf.Pow(miningDurationMultiplicator, _minedStonesCount));
    }
}