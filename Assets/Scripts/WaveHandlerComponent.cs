using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class WaveHandlerComponent : MonoBehaviour
{
    [Serializable]
    private struct WaveData
    {
        public int enemiesToKillUntilBoss;
        public float spawnInterval;
        public int maxAmountOfSimultaneouslyLivingEnemies;
        public GameObject bossPrefab;
        public float rangeBegin;
        public float rangeEnd;
        public float rangeDecuct;
    }

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<WaveData> data;

    private DataHandlerComponent _dataHandlerComponent;
    //private float _elapsedTime; 7.10 Sekunden, Zwischen Spawn und Tod mit Mauer
    private int _killedEnemiesDuringWave;
    private bool _bossFightEnabled;
    private float _timeBetweenSpawn;

    public delegate void SpawnEnemy(GameObject enemyPrefab, int maxAmountOfSimultaneouslyLivingEnemies,
        int killedEnemies, int enemiesToKillUntilBoss);

    public static SpawnEnemy OnSpawnEnemy;
    

    private void Start()
    {
        EnemyController.OnEnemyDestroyed += OnEnemyDestroyed;
        BossComponent.OnBossDestroyed += OnBossDestroyed;

        _dataHandlerComponent = GameObject.FindWithTag("DataHandler").GetComponent<DataHandlerComponent>();
        StartCoroutine("FirstWave");
    }

    private void OnBossDestroyed()
    {
        _bossFightEnabled = false;
        //_elapsedTime = 0f;
    }

    private void OnEnemyDestroyed(int objectId)
    {
        _killedEnemiesDuringWave++;
    }

    /*
    private void FixedUpdate()
    {
        if (_bossFightEnabled) return;

        _elapsedTime += Time.deltaTime;

        var currentData = data[_dataHandlerComponent.Wave];
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
    */

    private void OnDestroy()
    {
        EnemyController.OnEnemyDestroyed -= OnEnemyDestroyed;
        BossComponent.OnBossDestroyed -= OnBossDestroyed;
    }

    //Coroutines to better support the spawnValues
    private IEnumerator FirstWave()
    {
        int counter = 0;
        while (true)
        {
            var currentData = data[_dataHandlerComponent.Wave];
            _timeBetweenSpawn = UnityEngine.Random.Range(currentData.rangeBegin -= currentData.rangeDecuct * 0.8f * counter, currentData.rangeEnd -= currentData.rangeDecuct * counter);
            counter++;
            Debug.Log("Time Between Spawn: " + _timeBetweenSpawn);
            //Is Counting Time, no need to count up DeltaTime
            yield return new WaitForSeconds(_timeBetweenSpawn);
            //If Bossfight is Enabled after killing x Amount of Enemies Start Next Wave
            if (_bossFightEnabled)
            {
                StartCoroutine("SecondWave");
                yield break;
            }

            //This is Obsolete because Coroutine Waits for x Seconds
            //if (_elapsedTime <= currentData.spawnInterval) return;

            var maxAmountOfSimultaneouslyLivingEnemies = currentData.maxAmountOfSimultaneouslyLivingEnemies;

            if (_killedEnemiesDuringWave < currentData.enemiesToKillUntilBoss)
            {
                OnSpawnEnemy?.Invoke(enemyPrefab, maxAmountOfSimultaneouslyLivingEnemies, _killedEnemiesDuringWave,
                    currentData.enemiesToKillUntilBoss);
                //_elapsedTime = 0f;
                yield return new WaitForSeconds(0);
            }
            else
            {
                _bossFightEnabled = true;
                _killedEnemiesDuringWave = 0;
                OnSpawnEnemy?.Invoke(currentData.bossPrefab, maxAmountOfSimultaneouslyLivingEnemies,
                    _killedEnemiesDuringWave, currentData.enemiesToKillUntilBoss);
                yield return new WaitForSeconds(15);
            }
        }
    }

    private IEnumerator SecondWave()
    {
        StopCoroutine("FirstWave");
        int counter = 0;
        while (true)
        {
            var currentData = data[_dataHandlerComponent.Wave];
            _timeBetweenSpawn = UnityEngine.Random.Range(currentData.rangeBegin * 0.8f * counter, currentData.rangeEnd -= currentData.rangeDecuct * counter);
            Debug.Log("Time Between Spawn: " + _timeBetweenSpawn);
            //Is Counting Time, no need to count up DeltaTime
            yield return new WaitForSeconds(_timeBetweenSpawn);
            //If Bossfight is Enabled after killing x Amount of Enemies Start Next Wave
            if (_bossFightEnabled)
            {
                StartCoroutine("ThirdWave");
                yield break;
            }
            
            //This is Obsolete because Coroutine Waits for x Seconds
            //if (_elapsedTime <= currentData.spawnInterval) return;

            var maxAmountOfSimultaneouslyLivingEnemies = currentData.maxAmountOfSimultaneouslyLivingEnemies;

            if (_killedEnemiesDuringWave < currentData.enemiesToKillUntilBoss)
            {
                OnSpawnEnemy?.Invoke(enemyPrefab, maxAmountOfSimultaneouslyLivingEnemies, _killedEnemiesDuringWave,
                    currentData.enemiesToKillUntilBoss);
                //_elapsedTime = 0f;
                yield return new WaitForSeconds(0);
            }
            else
            {
                _bossFightEnabled = true;
                _killedEnemiesDuringWave = 0;
                OnSpawnEnemy?.Invoke(currentData.bossPrefab, maxAmountOfSimultaneouslyLivingEnemies,
                    _killedEnemiesDuringWave, currentData.enemiesToKillUntilBoss);
                yield return new WaitForSeconds(20);
            }
        }
    }
    private IEnumerator ThirdWave()
    {
        StopCoroutine("SecondWave");
        int counter = 0;
        while (true)
        {
            var currentData = data[_dataHandlerComponent.Wave];
            _timeBetweenSpawn = UnityEngine.Random.Range(currentData.rangeBegin * 0.8f * counter, currentData.rangeEnd -= currentData.rangeDecuct * counter);
            Debug.Log("Time Between Spawn: " + _timeBetweenSpawn);
            //Is Counting Time, no need to count up DeltaTime
            yield return new WaitForSeconds(_timeBetweenSpawn);
            //If Bossfight is Enabled after killing x Amount of Enemies Start Next Wave
            if (_bossFightEnabled)
            {
                yield break;
            }

            //This is Obsolete because Coroutine Waits for x Seconds
            //if (_elapsedTime <= currentData.spawnInterval) return;

            var maxAmountOfSimultaneouslyLivingEnemies = currentData.maxAmountOfSimultaneouslyLivingEnemies;

            if (_killedEnemiesDuringWave < currentData.enemiesToKillUntilBoss)
            {
                OnSpawnEnemy?.Invoke(enemyPrefab, maxAmountOfSimultaneouslyLivingEnemies, _killedEnemiesDuringWave,
                    currentData.enemiesToKillUntilBoss);
                //_elapsedTime = 0f;
                yield return new WaitForSeconds(0);
            }
            else
            {
                _bossFightEnabled = true;
                _killedEnemiesDuringWave = 0;
                OnSpawnEnemy?.Invoke(currentData.bossPrefab, maxAmountOfSimultaneouslyLivingEnemies,
                    _killedEnemiesDuringWave, currentData.enemiesToKillUntilBoss);
                yield return new WaitForSeconds(_timeBetweenSpawn);
            }
        }
    }

}