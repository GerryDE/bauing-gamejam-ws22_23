using UnityEngine;

namespace Animations
{
    public class EnemyAnimationComponent : MonoBehaviour
    {
        public delegate void EnemyDeathAnimationFinished(int objectId);
        public static EnemyDeathAnimationFinished OnEnemyDeathAnimationFinished;
        
        public void DeathAnimationFinished()
        {
            OnEnemyDeathAnimationFinished?.Invoke(transform.parent.gameObject.GetInstanceID());
        }
    }
}