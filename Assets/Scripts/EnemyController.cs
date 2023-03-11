using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    public int maxHp = 10;
    public float moveSpeed = 100f;
    public Vector2 throwBackForce = new Vector2(-100f, 0f);

    private Rigidbody2D _rigidbody;
    [SerializeField] private int currentHp;
    private bool _shallBeDestroyed;
    
    public delegate void EnemyDestroyed(int objectId);

    public static EnemyDestroyed OnEnemyDestroyed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        currentHp = maxHp;
    }

    private void FixedUpdate()
    {
        if (_shallBeDestroyed)
        {
            Destroy(gameObject);
            return;
        }
        _rigidbody.velocity = new Vector2(moveSpeed * Time.deltaTime, _rigidbody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Fence")))
        {
            _rigidbody.AddForce(throwBackForce);
        }

        if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            currentHp--;
            if (currentHp <= 0 && !_shallBeDestroyed)
            {
                _shallBeDestroyed = true;
                OnEnemyDestroyed?.Invoke(gameObject.GetInstanceID());
                return;
            }

            _rigidbody.AddForce(throwBackForce);
        }
    }
}