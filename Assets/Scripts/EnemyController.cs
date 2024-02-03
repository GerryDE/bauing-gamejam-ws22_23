using System;
using Data;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    public EnemyData Data;
    [SerializeField] private EnemyHpBar hpBar;
    [SerializeField] private SpriteRenderer backgroundRenderer;
    [SerializeField] private SpriteRenderer foregroundRenderer;



    private Rigidbody2D _rigidbody;
    private int _maxHp;
    private int _currentHp;
    private float _moveSpeed;
    private Vector2 _throwBackForce;

    public delegate void CollisionBetweenEnemyAndPlayer(Transform enemyTransform, Transform playerTransform);
    public delegate void CollisionBetweenEnemyAndFence(Transform enemyTransform, Transform fenceTransform);
    public delegate void EnemyDestroyed(int objectId);

    public static CollisionBetweenEnemyAndPlayer OnCollisionBetweenEnemyAndPlayer;
    public static CollisionBetweenEnemyAndFence OnCollisionBetweenEnemyAndFence;
    public static EnemyDestroyed OnEnemyDestroyed;

    private DataHandlerComponent _dataHandlerComponent;

    private void Awake()
    {
        _dataHandlerComponent = GameObject.FindWithTag("DataHandler").GetComponent<DataHandlerComponent>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _maxHp = Data.maxHp;
        _currentHp = Data.maxHp;
        _moveSpeed = Data.moveSpeed;
        _throwBackForce = Data.throwBackForce;

        DamageHandlerComponent.OnDealDamageToEnemy += OnDealDamageToEnemy;
    }

    private void OnDealDamageToEnemy(Transform enemyTransform, int damageValue)
    {
        if (!enemyTransform.GetInstanceID().Equals(transform.GetInstanceID())) return;
        ReduceHp(damageValue);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_moveSpeed * Time.deltaTime, _rigidbody.velocity.y);
    }

    private void ReduceHp(int value)
    {
        _currentHp -= value;
        
        if (_currentHp > 0) return;
        OnEnemyDestroyed?.Invoke(gameObject.GetInstanceID());
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Fence")))
        {
            OnCollisionBetweenEnemyAndFence(transform, col.transform);
            _rigidbody.AddForce(_throwBackForce);
            _dataHandlerComponent.PlayAttackAudioClip();
            hpBar.UpdateValues(_currentHp, _maxHp);
        }

        if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            OnCollisionBetweenEnemyAndPlayer?.Invoke(transform, col.transform);
            _rigidbody.AddForce(_throwBackForce);
            _dataHandlerComponent.PlayAttackPlayerAudioClip();
            hpBar.UpdateValues(_currentHp, _maxHp);
        }
    }

    private void OnDestroy()
    {
        DamageHandlerComponent.OnDealDamageToEnemy -= OnDealDamageToEnemy;
    }
}