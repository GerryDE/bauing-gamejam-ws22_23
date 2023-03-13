using System;
using UnityEngine;

public class EnemySpawnerComponent : MonoBehaviour
{
    private void Start()
    {
        WaveHandlerComponent.OnSpawnEnemy += SpawnEnemy;
    }

    private void SpawnEnemy(GameObject enemyPrefab, int maxAmountOfSimultaneouslyLivingEnemies, int killedEnemiesDuringWave, int enemiesToKillUntilBoss)
    {
        var length = transform.childCount;
        if (length < maxAmountOfSimultaneouslyLivingEnemies &&
           length + killedEnemiesDuringWave < enemiesToKillUntilBoss)
        {
            Instantiate(enemyPrefab, transform);
        }
    }

    private void OnDestroy()
    {
        WaveHandlerComponent.OnSpawnEnemy -= SpawnEnemy;
    }
}
