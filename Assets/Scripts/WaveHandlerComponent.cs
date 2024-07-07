using UnityEngine;
using static UnityEngine.Debug;

public class WaveHandlerComponent : MonoBehaviour
{
	public delegate void SpawnEnemy(GameObject enemyPrefab, int maxAmountOfSimultaneouslyLivingEnemies);

    public delegate void EnemySubWaveDefeated();

    public static SpawnEnemy OnSpawnEnemy;
    public static EnemySubWaveDefeated OnEnemySubWaveDefeated;
    
    private float _elapsedTime;
    private float _timeBetweenSpawn;
    private bool _activeSubWave;
    private int _subWaveEnemyCount;
    private int _enemiesDestroyedDuringSubWave;
    private DataProvider _dataProvider;
    private float _subWaveInterval;
    private float _spawnInterval;

    private void Start()
    {
        _dataProvider = DataProvider.Instance;
        if (!_dataProvider)
        {
            LogError("DataProvider not found!");
        }

        EnemyController.OnEnemyDestroyed += OnEnemyDestroyed;
        
        GenerateRandomSubWaveInterval();
        GenerateRandomSpawnInterval();
    }

    private void OnEnemyDestroyed(int objectId)
    {
        _enemiesDestroyedDuringSubWave++;

        if (_enemiesDestroyedDuringSubWave < _dataProvider.CurrentSubWaveData().enemies.Count) return;

        _activeSubWave = false;
        _enemiesDestroyedDuringSubWave = 0;
        OnEnemySubWaveDefeated?.Invoke();

        if (_dataProvider.SubWaveCount + 1 < _dataProvider.CurrentWaveData().subWaves.Count)
        {
            _dataProvider.SubWaveCount++;
        }
        else
        {
            _dataProvider.SubWaveCount = 0;
            if (_dataProvider.WaveCount + 1 >= _dataProvider.EnemySpawnWaveDataList.Count) return;
            _dataProvider.WaveCount++;
        }

        _elapsedTime = 0;
    }

    private void FixedUpdate()
    {
        if (!_dataProvider) return;

        var currentWaveData = _dataProvider.CurrentWaveData();
        if (currentWaveData.tutorial) return;

        var subWaveCount = _dataProvider.SubWaveCount;
        if (!_activeSubWave && subWaveCount < currentWaveData.subWaves.Count &&
            _elapsedTime >= _subWaveInterval)
        {
            _activeSubWave = true;
            _subWaveEnemyCount = 0;
            _elapsedTime = 0;
            GenerateRandomSubWaveInterval();
        }

        if (_activeSubWave)
        {
            var currentSubWave = _dataProvider.CurrentSubWaveData();
            if (_subWaveEnemyCount < currentSubWave.enemies.Count)
            {
                if (_elapsedTime >= _spawnInterval)
                {
                    OnSpawnEnemy?.Invoke(currentSubWave.enemies[_subWaveEnemyCount],
                        currentWaveData.maxAmountOfSimultaneouslyLivingEnemies);
                    _subWaveEnemyCount++;
                    _elapsedTime = 0;
                    GenerateRandomSpawnInterval();
                }
            }
        }

        _elapsedTime += Time.deltaTime;
    }

    private void GenerateRandomSubWaveInterval() 
    {
		var range = _dataProvider.CurrentWaveData().subWaveIntervalRange;
		float value = Random.Range(range.min, range.max);
        _subWaveInterval = value;
    }
    
    private void GenerateRandomSpawnInterval() 
    {
		var range = _dataProvider.CurrentSubWaveData().spawnIntervalRange;
		var value = Random.Range(range.min, range.max);
        _spawnInterval = value;
    }
}