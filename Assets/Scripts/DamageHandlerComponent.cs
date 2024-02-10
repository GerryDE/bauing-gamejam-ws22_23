using System;
using UnityEngine;

public class DamageHandlerComponent : MonoBehaviour
{
    [SerializeField] private float damageMultiplier = 1.5f;

    public delegate void DealDamageToPlayer(Transform playerTransform, int damageValue);

    public delegate void DealDamageToEnemy(Transform enemyTransform, int damageValue);

    public delegate void DealDamageToFence(Transform enemyTransform, int damageValue);

    public static DealDamageToPlayer OnDealDamageToPlayer;
    public static DealDamageToEnemy OnDealDamageToEnemy;
    public static DealDamageToFence OnDealDamageToFence;

    private void Start()
    {
        PlayerController.OnCollisionBetweenPlayerAndEnemy += OnCollisionBetweenPlayerAndEnemy;
        FenceController.OnCollisionBetweenFenceAndEnemy += OnCollisionBetweenFenceAndEnemy;
        EnemyController.OnCollisionBetweenEnemyAndPlayer += OnCollisionBetweenEnemyAndPlayer;
        EnemyController.OnCollisionBetweenEnemyAndFence += OnCollisionBetweenEnemyAndFence;
    }

    private void OnCollisionBetweenPlayerAndEnemy(Transform playerTransform, Transform enemyTransform)
    {
        var playerData = DataProvider.Instance.PlayerData;
        var enemyData = enemyTransform.gameObject.GetComponent<EnemyController>().Data;
        var damageToPlayer = CalculateDamage(enemyData.attack, playerData.DefenseValue);
        OnDealDamageToPlayer?.Invoke(playerTransform, damageToPlayer);
    }

    private void OnCollisionBetweenFenceAndEnemy(int index, Transform fenceTransform, Transform enemyTransform)
    {
        var enemyData = enemyTransform.gameObject.GetComponent<EnemyController>().Data;
        var fenceDataIndex = DataProvider.Instance.FenceData[index];
        var fenceData = fenceDataIndex.data[fenceDataIndex.version];
        var damageToEnemy = CalculateDamage(fenceData.damage, enemyData.defense);
        OnDealDamageToEnemy?.Invoke(enemyTransform, damageToEnemy);
    }

    private void OnCollisionBetweenEnemyAndFence(int index, Transform enemyTransform, Transform fenceTransform)
    {
        var enemyData = enemyTransform.gameObject.GetComponent<EnemyController>().Data;
        var fenceDataIndex = DataProvider.Instance.FenceData[index];
        var fenceData = fenceDataIndex.data[fenceDataIndex.version];
        var damageToFence = CalculateDamage(enemyData.attack, fenceData.defense);
        OnDealDamageToFence?.Invoke(fenceTransform, damageToFence);
    }

    private void OnCollisionBetweenEnemyAndPlayer(Transform enemyTransform, Transform playerTransform)
    {
        var playerData = DataProvider.Instance.PlayerData;
        var enemyData = enemyTransform.gameObject.GetComponent<EnemyController>().Data;
        var damageToEnemy = CalculateDamage(playerData.AttackValue, enemyData.defense);
        OnDealDamageToEnemy?.Invoke(enemyTransform, damageToEnemy);
    }

    private int CalculateDamage(int attack, int defense)
    {
        if (attack <= 0)
        {
            return 0;
        }
        
        return (int) Math.Max(1, Math.Pow(attack - defense, damageMultiplier));
    }

    private void OnDestroy()
    {
        PlayerController.OnCollisionBetweenPlayerAndEnemy -= OnCollisionBetweenPlayerAndEnemy;
        FenceController.OnCollisionBetweenFenceAndEnemy -= OnCollisionBetweenFenceAndEnemy;
        EnemyController.OnCollisionBetweenEnemyAndPlayer -= OnCollisionBetweenEnemyAndPlayer;
        EnemyController.OnCollisionBetweenEnemyAndFence -= OnCollisionBetweenEnemyAndFence;
    }
}