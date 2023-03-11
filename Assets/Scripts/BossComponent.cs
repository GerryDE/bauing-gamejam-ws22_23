using UnityEngine;

public class BossComponent : MonoBehaviour
{
    public delegate void BossDestroyed();

    public static BossDestroyed OnBossDestroyed;

    private void Start()
    {
        EnemyController.OnEnemyDestroyed += HandleBossDestroyed;
    }

    private void HandleBossDestroyed(int objectId)
    {
        if (objectId.Equals(gameObject.GetInstanceID()))
        {
            OnBossDestroyed?.Invoke();
        }
    }

    private void OnDestroy()
    {
        EnemyController.OnEnemyDestroyed -= HandleBossDestroyed;
    }
}