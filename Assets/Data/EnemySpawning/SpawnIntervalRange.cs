using System;
using UnityEngine;

namespace Data.EnemySpawning
{
    [Serializable]
    public struct SpawnIntervalRange
    {
        [Range(0f, 100f)]
        public float min, max;
    }
}