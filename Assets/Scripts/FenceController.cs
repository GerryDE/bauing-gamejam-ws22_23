using System;
using UnityEngine;

[RequireComponent(typeof(ProgressBarComponent))]
public class FenceController : MonoBehaviour
{
    [SerializeField, Range(0, 1000)] private int maxHp = 100;
    [SerializeField] private int currentHp;

    [SerializeField] private SpriteRenderer _renderer;
    
    private ProgressBarComponent _progressBarComponent;

    public int MaxHp
    {
        get => maxHp;
        private set => maxHp = value;
    }

    public int CurrentHp
    {
        get => currentHp;
        private set => currentHp = value;
    }

    private void Start()
    {
        _progressBarComponent = GetComponent<ProgressBarComponent>();
        _progressBarComponent.Enable();
        _progressBarComponent.UpdateValues(currentHp, MaxHp);
    }

    private void Awake()
    {
        FenceRepairComponent.OnRepairFence += OnRepairFence;
        FenceUpgradeComponent.OnUpgradeFence += OnUpgradeFence;

        currentHp = maxHp;
    }

    private void OnUpgradeFence(int newHpValue, Sprite sprite)
    {
        MaxHp = newHpValue;
        CurrentHp = MaxHp;
        _renderer.sprite = sprite;
        _progressBarComponent.UpdateValues(currentHp, MaxHp);
    }

    private void OnRepairFence(int amount)
    {
        currentHp = Math.Min(currentHp + amount, maxHp);
        _progressBarComponent.UpdateValues(currentHp, MaxHp);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy"))) return;

        currentHp -= 1;
        _progressBarComponent.UpdateValues(currentHp, MaxHp);

        if (currentHp > 0) return;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        FenceRepairComponent.OnRepairFence -= OnRepairFence;
        FenceUpgradeComponent.OnUpgradeFence -= OnUpgradeFence;
    }
}