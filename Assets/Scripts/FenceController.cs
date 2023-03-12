using System;
using UnityEngine;

public class FenceController : MonoBehaviour
{
    [Range(0, 1000)] public int maxHp = 100;

    public int MaxHp => maxHp;
    public int CurrentHp => currentHp;

    [SerializeField] private int currentHp;

    private void Awake()
    {
        FenceRepairComponent.OnRepairFence += OnRepairFence;

        currentHp = maxHp;
    }

    private void OnRepairFence(int amount)
    {
        currentHp = Math.Min(currentHp + amount, maxHp);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy"))) return;

        currentHp -= 1;

        if (currentHp > 0) return;
        Destroy(gameObject);
    }
}