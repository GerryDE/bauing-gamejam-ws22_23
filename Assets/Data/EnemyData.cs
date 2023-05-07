using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Assets/Data/EnemyData", menuName = "Data/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public int maxHp = 10;
        public int attack = 1;
        public int defense = 1;
        public int moveSpeed = 50;
        public Vector2 throwBackForce = new(-700f, 0f);
    }
}