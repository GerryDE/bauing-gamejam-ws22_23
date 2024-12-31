using System;
using System.Collections;
using Objective;
using TMPro;
using UnityEngine;

public class EnemySpawnerComponent : MonoBehaviour
{
    [SerializeField] GameObject prefabHowToKill;

    private void OnEnable()
    {
        WaveHandlerComponent.OnSpawnEnemy += SpawnEnemy;
        DefeatEnemyObjectiveHandler.OnSpawnEnemy += SpawnEnemy;
        Transform transform = gameObject.GetComponent<Transform>();
        // StartCoroutine(showInfo(prefabHowToKill, transform));
    }

    private void SpawnEnemy(GameObject enemyPrefab, int maxAmountOfSimultaneouslyLivingEnemies)
    {
        Instantiate(enemyPrefab, transform);
    }

    private void OnDisable()
    {
        WaveHandlerComponent.OnSpawnEnemy -= SpawnEnemy;
        DefeatEnemyObjectiveHandler.OnSpawnEnemy -= SpawnEnemy;
    }

    IEnumerator showInfo(GameObject prefabText, Transform parentTrans)
    {
        yield return new WaitForSeconds(7f);
        GameObject newObject = Instantiate(prefabText, new Vector3(-5.41f, -3.32f, 0), Quaternion.identity);
        yield return new WaitForSeconds(10f);
        Destroy(newObject);
        yield break;
    }
}
