using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private int maxHp = 10;
    [SerializeField] private float moveSpeed = 100f;
    [SerializeField] private int damageOutput = 30;
    [SerializeField] private Vector2 throwBackForce = new Vector2(-100f, 0f);
    [SerializeField] private EnemyHpBar hpBar;
    
    [SerializeField] private SpriteRenderer backgroundRenderer;
    [SerializeField] private SpriteRenderer foregroundRenderer;

    private Rigidbody2D _rigidbody;
    [SerializeField] private int currentHp;
    private bool _shallBeDestroyed;

    public delegate void EnemyDestroyed(int objectId);

    public delegate void ReducePlayerLifetime(int amount);

    public static EnemyDestroyed OnEnemyDestroyed;
    public static ReducePlayerLifetime OnReducePlayerLifetime;

    private DataHandlerComponent _dataHandlerComponent;

    private void Awake()
    {
        _dataHandlerComponent = GameObject.FindWithTag("DataHandler").GetComponent<DataHandlerComponent>();
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
            currentHp -= col.gameObject.GetComponent<FenceController>().DamageOutput;
            hpBar.UpdateValues(currentHp, maxHp);
            _rigidbody.AddForce(throwBackForce);
            _dataHandlerComponent.PlayAttackAudioClip();
        }

        if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            OnReducePlayerLifetime?.Invoke(damageOutput);

            currentHp--;

            _rigidbody.AddForce(throwBackForce);
            _dataHandlerComponent.PlayAttackPlayerAudioClip();
            hpBar.UpdateValues(currentHp, maxHp);
        }

        if (currentHp > 0 || _shallBeDestroyed) return;
        _shallBeDestroyed = true;
        OnEnemyDestroyed?.Invoke(gameObject.GetInstanceID());
    }
}