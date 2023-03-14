using UnityEngine;

public class EnemyFactoryComponent : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private float spawnInterval = 0.5f;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private int enemyAmountUntilBoss = 10;
    private float _elapsedTime;
    private int _spawnedEnemiesCount;

    private void FixedUpdate()
    {
        _elapsedTime += Time.deltaTime;
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

            _elapsedTime = 0f;
        }
    }
}