using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class EnemySpawnerComponent : MonoBehaviour
{
    [SerializeField] GameObject prefabHowToKill;

    private void Start()
    {
        WaveHandlerComponent.OnSpawnEnemy += SpawnEnemy;
        Transform transform = gameObject.GetComponent<Transform>();
        StartCoroutine(showInfo(prefabHowToKill, transform));
    }

    private void SpawnEnemy(GameObject enemyPrefab, int maxAmountOfSimultaneouslyLivingEnemies, int killedEnemiesDuringWave, int enemiesToKillUntilBoss)
    {
        var length = transform.childCount;
        if (length < maxAmountOfSimultaneouslyLivingEnemies &&
           length + killedEnemiesDuringWave < enemiesToKillUntilBoss)
        {
            Instantiate(enemyPrefab, transform);
        }
    }

    private void OnDestroy()
    {
        WaveHandlerComponent.OnSpawnEnemy -= SpawnEnemy;
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
