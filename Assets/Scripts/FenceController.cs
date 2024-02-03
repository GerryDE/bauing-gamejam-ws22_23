using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(ProgressBarComponent))]
public class FenceController : MonoBehaviour
{
    public int fenceIndex;
    [SerializeField, Range(0, 1000)] private int maxHp = 100;
    [SerializeField] private int currentHp;
    [SerializeField] private int damageOutput = 1;
    [SerializeField] private Color destroyedFenceColor;
    private Collider2D _collider2D;

    public int DamageOutput => damageOutput;

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
        _collider2D = GetComponent<Collider2D>();
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

    private void OnUpgradeFence(int index, int newHpValue, int damage, Sprite sprite)
    {
        if (index != fenceIndex) return;
        
        MaxHp = newHpValue;
        CurrentHp = MaxHp;
        damageOutput = damage;
        _renderer.sprite = sprite;
        _collider2D.enabled = true;
        _renderer.color = new Color(1f, 1f ,1f, 1f);
        _progressBarComponent.Enable();
        _progressBarComponent.UpdateValues(currentHp, MaxHp);
    }

    private void OnRepairFence(int index, int amount)
    {
        if (index != fenceIndex) return;
        currentHp = Math.Min(currentHp + amount, maxHp);
        _progressBarComponent.Enable();
        _progressBarComponent.UpdateValues(currentHp, MaxHp);
        _collider2D.enabled = true;
        _renderer.color = new Color(1f, 1f ,1f, 1f);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy"))) return;

        currentHp -= 1;
        _progressBarComponent.UpdateValues(currentHp, MaxHp);

        if (currentHp > 0) return;
        _collider2D.enabled = false;
        _renderer.color = destroyedFenceColor;
        _progressBarComponent.Disable();
    }

    private void OnDestroy()
    {
        FenceRepairComponent.OnRepairFence -= OnRepairFence;
        FenceUpgradeComponent.OnUpgradeFence -= OnUpgradeFence;
    }
}