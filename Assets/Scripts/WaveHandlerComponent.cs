using System.Collections.Generic;
using Data.EnemySpawning;
using UnityEngine;

public class WaveHandlerComponent : MonoBehaviour
{
    // public enum EnemyType
    // {
    //     ENEMY_1
    // }
    //
    // [Serializable]
    // public struct SpawnIntervalRange
    // {
    //     [Range(0f, 100f)]
    //     public float min, max;
    // }
    //
    // [Serializable]
    // private struct SubWave
    // {
    //     public int spawnAmount;
    //     public EnemyType EnemyType;
    //     public SpawnIntervalRange spawnIntervalRange;
    // }
    //
    // [Serializable]
    // private struct WaveData
    // {
    //     public bool tutorial;
    //     public int enemiesToKillUntilBoss;
    //     public float spawnInterval;
    //     public SpawnIntervalRange SpawnIntervalRange;
    //     public int maxAmountOfSimultaneouslyLivingEnemies;
    //     public List<SubWave> subWaves;
    //     public GameObject bossPrefab;
    // }

    [SerializeField] private GameObject enemyPrefab;
    private List<EnemySpawnWaveData> _spawnWaveData;

    private DataHandlerComponent _dataHandlerComponent;
    private float _elapsedTime;
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

        _spawnWaveData = DataProvider.Instance.EnemySpawnWaveDataList;

        _dataHandlerComponent = GameObject.FindWithTag("DataHandler").GetComponent<DataHandlerComponent>();
    }

    private void OnBossDestroyed()
    {
        _bossFightEnabled = false;
        _elapsedTime = 0f;
        DataProvider.Instance.Wave++;
    }

    private void OnEnemyDestroyed(int objectId)
    {
        _killedEnemiesDuringWave++;
    }

    private void FixedUpdate()
    {
        if (_bossFightEnabled) return;

        var currentData = _spawnWaveData[DataProvider.Instance.Wave];
        if (currentData.tutorial) return;

        _elapsedTime += Time.deltaTime;
        if (_elapsedTime <= currentData.spawnIntervalRange.min) return;

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

    // //Coroutines to better support the spawnValues
    // private IEnumerator FirstWave()
    // {
    //     int counter = 0;
    //     while (true)
    //     {
    //         var currentData = data[_dataHandlerComponent.Wave];
    //         _timeBetweenSpawn = UnityEngine.Random.Range(currentData.rangeBegin -= currentData.rangeDecuct * 0.8f * counter, currentData.rangeEnd -= currentData.rangeDecuct * counter);
    //         counter++;
    //         Debug.Log("Time Between Spawn: " + _timeBetweenSpawn);
    //         //Is Counting Time, no need to count up DeltaTime
    //         yield return new WaitForSeconds(_timeBetweenSpawn);
    //         //If Bossfight is Enabled after killing x Amount of Enemies Start Next Wave
    //         if (_bossFightEnabled)
    //         {
    //             StartCoroutine("SecondWave");
    //             yield break;
    //         }
    //
    //         //This is Obsolete because Coroutine Waits for x Seconds
    //         //if (_elapsedTime <= currentData.spawnInterval) return;
    //
    //         var maxAmountOfSimultaneouslyLivingEnemies = currentData.maxAmountOfSimultaneouslyLivingEnemies;
    //
    //         if (_killedEnemiesDuringWave < currentData.enemiesToKillUntilBoss)
    //         {
    //             OnSpawnEnemy?.Invoke(enemyPrefab, maxAmountOfSimultaneouslyLivingEnemies, _killedEnemiesDuringWave,
    //                 currentData.enemiesToKillUntilBoss);
    //             //_elapsedTime = 0f;
    //             yield return new WaitForSeconds(0);
    //         }
    //         else
    //         {
    //             _bossFightEnabled = true;
    //             _killedEnemiesDuringWave = 0;
    //             OnSpawnEnemy?.Invoke(currentData.bossPrefab, maxAmountOfSimultaneouslyLivingEnemies,
    //                 _killedEnemiesDuringWave, currentData.enemiesToKillUntilBoss);
    //             yield return new WaitForSeconds(15);
    //         }
    //     }
    // }
    //
    // private IEnumerator SecondWave()
    // {
    //     StopCoroutine("FirstWave");
    //     int counter = 0;
    //     while (true)
    //     {
    //         var currentData = data[_dataHandlerComponent.Wave];
    //         _timeBetweenSpawn = UnityEngine.Random.Range(currentData.rangeBegin * 0.8f * counter, currentData.rangeEnd -= currentData.rangeDecuct * counter);
    //         Debug.Log("Time Between Spawn: " + _timeBetweenSpawn);
    //         //Is Counting Time, no need to count up DeltaTime
    //         yield return new WaitForSeconds(_timeBetweenSpawn);
    //         //If Bossfight is Enabled after killing x Amount of Enemies Start Next Wave
    //         if (_bossFightEnabled)
    //         {
    //             StartCoroutine("ThirdWave");
    //             yield break;
    //         }
    //         
    //         //This is Obsolete because Coroutine Waits for x Seconds
    //         //if (_elapsedTime <= currentData.spawnInterval) return;
    //
    //         var maxAmountOfSimultaneouslyLivingEnemies = currentData.maxAmountOfSimultaneouslyLivingEnemies;
    //
    //         if (_killedEnemiesDuringWave < currentData.enemiesToKillUntilBoss)
    //         {
    //             OnSpawnEnemy?.Invoke(enemyPrefab, maxAmountOfSimultaneouslyLivingEnemies, _killedEnemiesDuringWave,
    //                 currentData.enemiesToKillUntilBoss);
    //             //_elapsedTime = 0f;
    //             yield return new WaitForSeconds(0);
    //         }
    //         else
    //         {
    //             _bossFightEnabled = true;
    //             _killedEnemiesDuringWave = 0;
    //             OnSpawnEnemy?.Invoke(currentData.bossPrefab, maxAmountOfSimultaneouslyLivingEnemies,
    //                 _killedEnemiesDuringWave, currentData.enemiesToKillUntilBoss);
    //             yield return new WaitForSeconds(20);
    //         }
    //     }
    // }
    // private IEnumerator ThirdWave()
    // {
    //     StopCoroutine("SecondWave");
    //     int counter = 0;
    //     while (true)
    //     {
    //         var currentData = data[_dataHandlerComponent.Wave];
    //         _timeBetweenSpawn = UnityEngine.Random.Range(currentData.rangeBegin * 0.8f * counter, currentData.rangeEnd -= currentData.rangeDecuct * counter);
    //         Debug.Log("Time Between Spawn: " + _timeBetweenSpawn);
    //         //Is Counting Time, no need to count up DeltaTime
    //         yield return new WaitForSeconds(_timeBetweenSpawn);
    //         //If Bossfight is Enabled after killing x Amount of Enemies Start Next Wave
    //         if (_bossFightEnabled)
    //         {
    //             yield break;
    //         }
    //
    //         //This is Obsolete because Coroutine Waits for x Seconds
    //         //if (_elapsedTime <= currentData.spawnInterval) return;
    //
    //         var maxAmountOfSimultaneouslyLivingEnemies = currentData.maxAmountOfSimultaneouslyLivingEnemies;
    //
    //         if (_killedEnemiesDuringWave < currentData.enemiesToKillUntilBoss)
    //         {
    //             OnSpawnEnemy?.Invoke(enemyPrefab, maxAmountOfSimultaneouslyLivingEnemies, _killedEnemiesDuringWave,
    //                 currentData.enemiesToKillUntilBoss);
    //             //_elapsedTime = 0f;
    //             yield return new WaitForSeconds(0);
    //         }
    //         else
    //         {
    //             _bossFightEnabled = true;
    //             _killedEnemiesDuringWave = 0;
    //             OnSpawnEnemy?.Invoke(currentData.bossPrefab, maxAmountOfSimultaneouslyLivingEnemies,
    //                 _killedEnemiesDuringWave, currentData.enemiesToKillUntilBoss);
    //             yield return new WaitForSeconds(_timeBetweenSpawn);
    //         }
    //     }
    // }
}