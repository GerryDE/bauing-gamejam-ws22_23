using System;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Assets/Data/EnemyData", menuName = "Data/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private int initialMaxHp = 10;
        [SerializeField] private int initialAttack = 1;
        [SerializeField] private int initialDefense = 1;
        [SerializeField] private int initialSpeed = 50;
        
        [NonSerialized] public int MaxHp;
        [NonSerialized] public int CurrentHp;
        [NonSerialized] public int Attack;
        [NonSerialized] public int Defense;
        [NonSerialized] public int Speed;

        private void Awake()
        {
            MaxHp = initialMaxHp;
            CurrentHp = MaxHp;
            Attack = initialAttack;
            Defense = initialDefense;
            Speed = initialSpeed;
        }
    }
}