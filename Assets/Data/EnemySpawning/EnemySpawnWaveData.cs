using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;

namespace Data.EnemySpawning
{
    [CreateAssetMenu(fileName = "Assets/Data/EnemySpawning/EnemySpawnWaveData", menuName = "Data/Enemy spawning/Enemy Spawn Wave")]
    public class EnemySpawnWaveData : ScriptableObject
    {
        public bool tutorial;
        public SpawnIntervalRange spawnIntervalRange;
        public int maxAmountOfSimultaneouslyLivingEnemies = 3;
        public List<EnemySpawnSubWaveData> subWaves = new List<EnemySpawnSubWaveData>();
        public GameObject bossPrefab;
        public int enemiesToKillUntilBoss;

        public EnemySpawnWaveData()
        {
            if (subWaves == null)
            {
                Log("You should add at least 1 Sub-wave to the current Enemy Spawn Wave data!");
                return;
            }
            
            foreach (var subWave in subWaves)
            {
                enemiesToKillUntilBoss += subWave.spawnAmount;
            }
        }
    }
}