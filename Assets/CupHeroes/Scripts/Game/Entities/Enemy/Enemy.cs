using System.Collections;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour, IEnemy
{
    private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private IEntityHealth _health;
    private IEntityStateMachine _stateMachine;

    private EnemyConfig _config;
    private IObjectPool _currentPool;

    [Inject]
    private void Construct(IEntityHealth health)
    {
        _health = health;
    }

    public Transform Transform => transform;
    public Collider2D Collider => _collider;
    public Rigidbody2D Rigidbody => _rigidbody;
    public Animator Animator => _animator;
    public EntityConfig Config => _config;
    public IEntityHealth Health => _health;
    public IEntityStateMachine StateMachine => _stateMachine;

    public void Initialize()
    {
        _collider = GetComponent<Collider2D>();
        Debug.Log($"Enemy Initialize. Collider = {Collider}");

        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _stateMachine = GetComponent<IEntityStateMachine>();

        _health.Initialize(this, _config.MainStats.BaseValueHealth);

        StartCoroutine(TakeDamage());
    }

    public void SetConfig(EntityConfig config)
    {
        if (config is EnemyConfig enemyConfig)
            _config = enemyConfig;
        else
            Debug.LogError("Invalid config type for Character");
    }

    public void SetPool(IObjectPool currentPool)
    {
        _currentPool = currentPool;
        _health.EntityDied += ReturnInPool;
    }

    private void ReturnInPool(IEntity entity)
    {
        _currentPool.ReturnPoolObject(entity as Enemy);
    }

    //TEST
    private IEnumerator TakeDamage()
    {
        yield return new WaitForSeconds(20f);

        float d = 50f;

        DamageData damageData = new DamageData(d);

        _health.TakeDamage(damageData);

        yield return new WaitForSeconds(10f);

        _health.TakeDamage(damageData);

        yield return new WaitForSeconds(10f); 

        _health.TakeDamage(damageData);
    }

    private void OnDisable()
    {
        _health = null;
        _config = null;
        _collider = null;
        _rigidbody = null;
        _animator = null;
        _stateMachine = null;

        if (_health != null)
            _health.EntityDied -= ReturnInPool;
    }
}
