using System;
using Data.objective;
using UnityEngine;

namespace Objective
{
    public class DefeatEnemyObjectiveHandler : ObjectiveHandler
    {
        private DefeatEnemyObjectiveData _data;

        public delegate void SpawnEnemy(GameObject enemyPrefab, int maxAmountOfSimultaneouslyLivingEnemies,
            int killedEnemiesDuringWave, int enemiesToKillUntilBoss);

        public static SpawnEnemy OnSpawnEnemy;

        public DefeatEnemyObjectiveHandler(DefeatEnemyObjectiveData data)
        {
            _data = data;
            
            OnSpawnEnemy?.Invoke(_data.enemyObj, 1, 0, 1);
            EnemyController.OnEnemyDestroyed += OnEnemyDestroyed;
        }

        private void OnEnemyDestroyed(int objectId)
        {
            OnObjectiveReached?.Invoke(_data);
            
            EnemyController.OnEnemyDestroyed -= OnEnemyDestroyed;
        }
    }
}