using System.Collections;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour, IEnemy
{
    private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private IEnemyHealth _health;
    private IEntityStateMachine _stateMachine;

    private EnemyConfig _config;
    private EnemyController _controller;
    private IObjectPool _currentPool;

    [Inject]
    private void Construct(IEnemyHealth health)
    {
        _health = health;
    }

    public Transform Transform => transform;
    public Collider2D Collider => _collider;
    public Rigidbody2D Rigidbody => _rigidbody;
    public Animator Animator => _animator;
    public EntityConfig Config => _config;
    public EnemyController Controller => _controller;
    public IEntityStateMachine StateMachine => _stateMachine;

    /// <summary>
    /// If you need to access EnemyHealth specific methods, you can cast it like this: 
    /// (IEnemyHealth)Health or health = entity.Health as IEnemyHealth.
    /// </summary>
    public IEntityHealth Health => _health;

    public void Initialize()
    {
        SetComponents();

        InitializeHealth();

        StartCoroutine(TakeDamage());
    }

    public void SetController(EnemyController controller) => _controller = controller;

    public void SetConfig(EntityConfig config)
    {
        if (config is EnemyConfig enemyConfig)
        {
            _config = enemyConfig;

            _health.SetConfig(enemyConfig);
        }
        else
        {
            Debug.LogError("Invalid config type for Character");
        }
    }

    public void SetPool(IObjectPool currentPool)
    {
        _currentPool = currentPool;
    }

    private void ReturnInPool(IEntity entity)
    {
        _currentPool.ReturnPoolObject(entity as Enemy);
    }

    private void SetComponents()
    {
        if (_collider != null)
            return;
        _collider = GetComponent<Collider2D>();

        if (_rigidbody != null)
            return;
        _rigidbody = GetComponent<Rigidbody2D>();

        if (_animator != null) 
            return;
        _animator = GetComponent<Animator>();

        if (_stateMachine != null)
            return;
        _stateMachine = GetComponent<IEntityStateMachine>();
    }

    private void InitializeHealth()
    {
        _health.Initialize(this, _config.MainStats.BaseValueHealth);

        _health.EntityDied += ReturnInPool;
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
        if (_stateMachine != null)
            _stateMachine.RemoveState();

        if (_health != null)
            _health.EntityDied -= ReturnInPool;
    }
}
