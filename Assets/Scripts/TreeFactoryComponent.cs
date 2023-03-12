using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreeFactoryComponent : MonoBehaviour
{
    [System.Serializable]
    private struct Range
    {
        public float min, max;
    }

    [SerializeField] private GameObject treePrefab;
    [SerializeField] private Range spawnRange;
    [SerializeField] private int defaultSpawnRate;
    [SerializeField] private float variance;

    private int _durationUntilNextSpawn;
    private int _elapsedTime;

    private int GenerateDurationUntilNextSpawn()
    {
        return (int) Random.Range(defaultSpawnRate * (1f - variance), defaultSpawnRate * (1f + variance));
    }

    private void InstantiateTree(int spawnXPos)
    {
        var xPos = Random.Range(spawnRange.min, spawnRange.max);
        var instance = Instantiate(treePrefab);
        instance.transform.position = Vector3.right * xPos;
    }

    private void Update()
    {
        _elapsedTime++;
    }
}
