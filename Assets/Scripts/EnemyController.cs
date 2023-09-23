using Data;
using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    [SerializeField] private EnemyHpBar hpBar;

    [SerializeField] private SpriteRenderer backgroundRenderer;
    [SerializeField] private SpriteRenderer foregroundRenderer;

    private Rigidbody2D _rigidbody;
    private int _maxHp;
    private int _currentHp;
    private int _attackValue;
    private int _defenseValue;
    private float _moveSpeed;
    private Vector2 _throwBackForce;
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

        _maxHp = data.maxHp;
        _currentHp = data.maxHp;
        _attackValue = data.attack;
        _defenseValue = data.defense;
        _moveSpeed = data.moveSpeed;
        _throwBackForce = data.throwBackForce;
    }

    private void FixedUpdate()
    {
        //if (_shallBeDestroyed)
        //{
        //    Destroy(this);
        //    return;
        //}

        _rigidbody.velocity = new Vector2(_moveSpeed * Time.deltaTime, _rigidbody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Fence")))
        {
            _currentHp -= col.gameObject.GetComponent<FenceController>().DamageOutput;
            hpBar.UpdateValues(_currentHp, _maxHp);
            _rigidbody.AddForce(_throwBackForce);
            _dataHandlerComponent.PlayAttackAudioClip();
        }

        if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            OnReducePlayerLifetime?.Invoke(_attackValue);

            _currentHp--;

            _rigidbody.AddForce(_throwBackForce);
            _dataHandlerComponent.PlayAttackPlayerAudioClip();
            hpBar.UpdateValues(_currentHp, _maxHp);
        }

        if (_currentHp > 0 || _shallBeDestroyed) return;
        _shallBeDestroyed = true;
        OnEnemyDestroyed?.Invoke(gameObject.GetInstanceID());
        Destroy(this);
    }
}