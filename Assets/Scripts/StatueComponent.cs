using UnityEngine;

public class StatueComponent : InteractableBaseComponent
{
    [SerializeField] private float startingPrayingDuration = 1f;
    [SerializeField] private float prayingDurationMultiplicator = 0.9f;
    [SerializeField] private SpriteRenderer _renderer;

    private float _elapsedTime;
    private float _prayingDuration;
    private int _prayCount;

    public delegate void Prayed(int amount);

    public static Prayed OnPrayed;

    protected override void Start()
    {
        base.Start();
        StatueUpgradeComponent.OnUpgradeStatue += OnUpgradeStatue;
        _prayingDuration = CalculatePrayingDuration();
    }

    private void OnUpgradeStatue(int newAgeValue, Sprite sprite)
    {
        _renderer.sprite = sprite;
    }

    private void FixedUpdate()
    {
        if (!_interaction1Enabled)
        {
            _prayCount = 0;
            return;
        }

        _elapsedTime += Time.deltaTime;
        if (_elapsedTime <= _prayingDuration) return;

        OnPrayed?.Invoke(1);
        _elapsedTime = 0;
        _prayCount++;
        _prayingDuration = CalculatePrayingDuration();
    }

    private float CalculatePrayingDuration()
    {
        return (startingPrayingDuration * Mathf.Pow(prayingDurationMultiplicator, _prayCount));
    }
}