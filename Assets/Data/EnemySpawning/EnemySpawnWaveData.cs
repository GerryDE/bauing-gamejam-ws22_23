using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using static UnityEngine.Debug;

namespace Data.EnemySpawning
{
    [CreateAssetMenu(fileName = "Assets/Data/EnemySpawning/EnemySpawnWaveData", menuName = "Data/Enemy spawning/Enemy Spawn Wave")]
    public class EnemySpawnWaveData : ScriptableObject
    {
        public bool tutorial;
        public SpawnIntervalRange subWaveIntervalRange;
        public int maxAmountOfSimultaneouslyLivingEnemies = 3;
        public List<EnemySpawnSubWaveData> subWaves = new List<EnemySpawnSubWaveData>();
    }
}