using UnityEngine;

public class EnemyFactoryComponent : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private int spawnInterval = 600;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private int enemyAmountUntilBoss = 10;
    private int _elapsedTime;
    private int _spawnedEnemiesCount;

    private void Update()
    {
        _elapsedTime++;
        if (_elapsedTime > spawnInterval)
        {
            GameObject enemy;
            if (_spawnedEnemiesCount >= enemyAmountUntilBoss)
            {
                enemy = Instantiate(bossPrefab);
                _spawnedEnemiesCount = 0;
            }
            else
            {
                enemy = Instantiate(enemyPrefab);
                _spawnedEnemiesCount++;
            }

            enemy.transform.position = spawnPosition;

            _elapsedTime = 0;
        }
    }
}