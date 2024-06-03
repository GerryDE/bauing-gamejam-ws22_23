using System;
using Animations;
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
    [SerializeField] private SpriteRenderer enemySprite;



    private Rigidbody2D _rigidbody;
    private int _maxHp;
    private int _currentHp;
    private float _moveSpeed;
    private Vector2 _throwBackForce;
    private bool _dying;

    public delegate void CollisionBetweenEnemyAndPlayer(Transform enemyTransform, Transform playerTransform);
    public delegate void CollisionBetweenEnemyAndFence(int index, Transform enemyTransform, Transform fenceTransform);
    public delegate void EnemyDestroyed(int objectId);

    public static CollisionBetweenEnemyAndPlayer OnCollisionBetweenEnemyAndPlayer;
    public static CollisionBetweenEnemyAndFence OnCollisionBetweenEnemyAndFence;
    public static EnemyDestroyed OnEnemyDestroyed;

    private DataHandlerComponent _dataHandlerComponent;
    private static readonly int Kill = Animator.StringToHash("kill");

    private void Awake()
    {
        _dataHandlerComponent = GameObject.FindWithTag("DataHandler").GetComponent<DataHandlerComponent>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _maxHp = Data.maxHp;
        _currentHp = Data.maxHp;
        _moveSpeed = Data.moveSpeed;
        _throwBackForce = Data.throwBackForce;

        DamageHandlerComponent.OnDealDamageToEnemy += OnDealDamageToEnemy;
        EnemyAnimationComponent.OnEnemyDeathAnimationFinished += OnEnemyDeathAnimationFinished;
    }

    private void OnDealDamageToEnemy(Transform enemyTransform, int damageValue)
    {
        if (!enemyTransform.GetInstanceID().Equals(transform.GetInstanceID())) return;
        ReduceHp(damageValue);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_moveSpeed * Time.deltaTime, 0f) * (_dying ? -2f : 1f);
    }

    private void ReduceHp(int value)
    {
        _currentHp -= value;
        
        if (_currentHp > 0) return;
        if (enemySprite.gameObject.TryGetComponent<Animator>(out var animator))
        {
            _dying = true;
            animator.SetTrigger(Kill);
            GetComponent<Collider2D>().enabled = false;
            backgroundRenderer.enabled = false;
            foregroundRenderer.enabled = false;
        }
        else
        {
            DestroyEnemy();
        }
    }
    
    private void OnEnemyDeathAnimationFinished(int objectId)
    {
        if (objectId != gameObject.GetInstanceID()) return;
        DestroyEnemy();
    }

    private void DestroyEnemy()
    {
        OnEnemyDestroyed?.Invoke(gameObject.GetInstanceID());
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Fence")))
        {
            var fenceIndex = 0;
            if (col.transform.TryGetComponent<FenceController>(out var fenceController))
            {
                fenceIndex = fenceController.fenceIndex;
            }
            OnCollisionBetweenEnemyAndFence(fenceIndex, transform, col.transform);
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
        EnemyAnimationComponent.OnEnemyDeathAnimationFinished -= OnEnemyDeathAnimationFinished;
    }
}