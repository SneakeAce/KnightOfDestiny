using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour, IEnemy
{
    private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private IAnimationEventReceiver _animationEventReceiver;
    private IEnemyHealth _health;
    private IEntityStateMachine _stateMachine;
    private IEntityController _entityController;

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
    public IAnimationEventReceiver AnimationEventReceiver => _animationEventReceiver;
    public IEntityStateMachine StateMachine => _stateMachine;

    /// <summary>
    /// If you need to access EnemyHealth specific methods, you can cast it like this: 
    /// (IEnemyHealth)Health or health = entity.Health as IEnemyHealth.
    /// </summary>
    public IEntityHealth Health => _health;
    public IEntityController EntityController => _entityController;


    public void Initialize()
    {
        SetComponents();

        InitializeHealth();
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

    public void SetController(IEntityController controller)
    {
        _entityController = controller;
    }

    private void ReturnInPool(IEntity entity)
    {
        _currentPool.ReturnPoolObject(entity as Enemy);
    }

    private void SetComponents()
    {
        if (_collider == null)
            _collider = GetComponent<Collider2D>();

        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();

        if (_animator == null) 
            _animator = GetComponent<Animator>();

        if (_animationEventReceiver == null)
            _animationEventReceiver = GetComponent<AnimationEventReceiver>();

        if (_stateMachine == null)
            _stateMachine = GetComponent<IEntityStateMachine>();
    }

    private void InitializeHealth()
    {
        _health.Initialize(this, _config.HealthStats.BaseValueHealth);

        _health.EntityDied += ReturnInPool;
    }

    private void OnDisable()
    {
        if (_stateMachine != null)
            _stateMachine.RemoveState();

        if (_health != null)
            _health.EntityDied -= ReturnInPool;
    }

}
