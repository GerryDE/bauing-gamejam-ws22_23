using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveHandlerComponent : MonoBehaviour
{
    [Serializable]
    private struct WaveData
    {
        public bool tutorial;
        public int enemiesToKillUntilBoss;
        public float spawnInterval;
        public int maxAmountOfSimultaneouslyLivingEnemies;
        public GameObject bossPrefab;
    }

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<WaveData> data;

    private DataHandlerComponent _dataHandlerComponent;
    private float _elapsedTime;
    private int _killedEnemiesDuringWave;
    private bool _bossFightEnabled;

    public delegate void SpawnEnemy(GameObject enemyPrefab, int maxAmountOfSimultaneouslyLivingEnemies,
        int killedEnemies, int enemiesToKillUntilBoss);

    public static SpawnEnemy OnSpawnEnemy;

    private void Start()
    {
        EnemyController.OnEnemyDestroyed += OnEnemyDestroyed;
        BossComponent.OnBossDestroyed += OnBossDestroyed;

        _dataHandlerComponent = GameObject.FindWithTag("DataHandler").GetComponent<DataHandlerComponent>();
    }

    private void OnBossDestroyed()
    {
        _bossFightEnabled = false;
        _elapsedTime = 0f;
    }

    private void OnEnemyDestroyed(int objectId)
    {
        _killedEnemiesDuringWave++;
    }

    private void FixedUpdate()
    {
        if (_bossFightEnabled) return;

        var currentData = data[_dataHandlerComponent.Wave];
        if (currentData.tutorial) return;
        
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime <= currentData.spawnInterval) return;

        var maxAmountOfSimultaneouslyLivingEnemies = currentData.maxAmountOfSimultaneouslyLivingEnemies;
        if (_killedEnemiesDuringWave < currentData.enemiesToKillUntilBoss)
        {
            OnSpawnEnemy?.Invoke(enemyPrefab, maxAmountOfSimultaneouslyLivingEnemies, _killedEnemiesDuringWave,
                currentData.enemiesToKillUntilBoss);
            _elapsedTime = 0f;
        }
        else
        {
            _bossFightEnabled = true;
            _killedEnemiesDuringWave = 0;
            OnSpawnEnemy?.Invoke(currentData.bossPrefab, maxAmountOfSimultaneouslyLivingEnemies,
                _killedEnemiesDuringWave, currentData.enemiesToKillUntilBoss);
        }

        _elapsedTime = 0f;
    }

    private void OnDestroy()
    {
        EnemyController.OnEnemyDestroyed -= OnEnemyDestroyed;
        BossComponent.OnBossDestroyed -= OnBossDestroyed;
    }
}