using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FenceController : MonoBehaviour
{
    [Range(0, 1000)] public int maxHp = 100;

    private Rigidbody2D _rigidbody;

    [SerializeField] private int currentHp;

    private void Awake()
    {
        FencePlayerInteractionListener.onHealFence += Heal;

        _rigidbody = GetComponent<Rigidbody2D>();
        currentHp = maxHp;
    }

    private void Heal()
    {
        currentHp = Math.Min(currentHp + 1, maxHp);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy"))) return;

        currentHp -= 1;

        if (currentHp > 0) return;
        Destroy(gameObject);
    }
}