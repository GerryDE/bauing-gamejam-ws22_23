using UnityEngine;
using static UnityEngine.Debug;

public class WaveHandlerComponent : MonoBehaviour
{
    private float _elapsedTime;
    private float _timeBetweenSpawn;
    private bool _activeSubWave;
    private int _subWaveEnemyCount;
    private int _enemiesDestroyedDuringSubWave;
    private DataProvider _dataProvider;

    public delegate void SpawnEnemy(GameObject enemyPrefab, int maxAmountOfSimultaneouslyLivingEnemies);

    public delegate void EnemySubWaveDefeated();

    public static SpawnEnemy OnSpawnEnemy;
    public static EnemySubWaveDefeated OnEnemySubWaveDefeated;

    private void Start()
    {
        _dataProvider = DataProvider.Instance;
        if (!_dataProvider)
        {
            LogError("DataProvider not found!");
        }

        EnemyController.OnEnemyDestroyed += OnEnemyDestroyed;
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
            _elapsedTime >= currentWaveData.subWaveIntervalRange.min)
        {
            _activeSubWave = true;
            _subWaveEnemyCount = 0;
            _elapsedTime = 0;
        }

        if (_activeSubWave)
        {
            var currentSubWave = _dataProvider.CurrentSubWaveData();
            if (_subWaveEnemyCount < currentSubWave.enemies.Count)
            {
                if (_elapsedTime >= currentSubWave.spawnIntervalRange.min)
                {
                    OnSpawnEnemy?.Invoke(currentSubWave.enemies[_subWaveEnemyCount],
                        currentWaveData.maxAmountOfSimultaneouslyLivingEnemies);
                    _subWaveEnemyCount++;
                    _elapsedTime = 0;
                }
            }
        }

        _elapsedTime += Time.deltaTime;
    }
}