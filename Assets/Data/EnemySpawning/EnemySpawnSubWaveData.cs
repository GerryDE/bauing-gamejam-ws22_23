using System.Collections.Generic;
using UnityEngine;
using static Data.EnemySpawning.EnemyType;

namespace Data.EnemySpawning
{
    public enum EnemyType
    {
        ENEMY_1
    }
    
    [CreateAssetMenu(fileName = "Assets/Data/EnemySpawning/EnemySpawnSubWaveData", menuName = "Data/Enemy spawning/Enemy Spawn Sub-wave")]
    public class EnemySpawnSubWaveData : ScriptableObject
    {
        public List<GameObject> enemies;
        public SpawnIntervalRange spawnIntervalRange;

        public int EnemyAmount()
        {
            return enemies.Count;
        }
    }
}