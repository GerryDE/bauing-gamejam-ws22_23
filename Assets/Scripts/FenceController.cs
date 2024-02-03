using System;
using Data.objective;
using Objective;
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

    public delegate void CurrentHpChanged(int value, int maxHp);

    public delegate void CollisionBetweenFenceAndEnemy(Transform fenceTransform, Transform enemyTransform);

    public static CurrentHpChanged OnCurrentHpChanged;
    public static CollisionBetweenFenceAndEnemy OnCollisionBetweenFenceAndEnemy;

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
        private set
        {
            currentHp = value;
            OnCurrentHpChanged?.Invoke(value, maxHp);
        }
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
        TutorialComponent.OnNewObjectiveStarted += OnObjectiveStarted;
        DamageHandlerComponent.OnDealDamageToFence += OnDealDamageToFence;

        currentHp = maxHp;
    }

    private void OnDealDamageToFence(Transform enemyTransform, int damageValue)
    {
        if (!enemyTransform.GetInstanceID().Equals(transform.GetInstanceID())) return;
        ReduceHp(damageValue);
    }

    private void OnObjectiveStarted(ObjectiveData data)
    {
        if (data.GetType() == typeof(TutorialCompletedObjectiveData))
        {
            CurrentHp = MaxHp;
        }
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
        OnCollisionBetweenFenceAndEnemy?.Invoke(transform, col.transform);
    }

    private void ReduceHp(int value)
    {
        CurrentHp -= value;
        _progressBarComponent.UpdateValues(CurrentHp, MaxHp);

        if (CurrentHp > 0) return;
        _collider2D.enabled = false;
        _renderer.color = destroyedFenceColor;
        _progressBarComponent.Disable();
    }

    private void OnDestroy()
    {
        FenceRepairComponent.OnRepairFence -= OnRepairFence;
        FenceUpgradeComponent.OnUpgradeFence -= OnUpgradeFence;
        ObjectiveHandler.OnObjectiveReached -= OnObjectiveStarted;
    }
}