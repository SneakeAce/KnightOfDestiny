using UnityEngine;
using Zenject;

public class Character : MonoBehaviour, ICharacter
{
    private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private CharacterConfig _config;

    private Transform _projectileSpawnPosition;

    private IAnimationEventReceiver _animationEventReceiver;
    private ICharacterHealth _health;
    private IEntityStateMachine _stateMachine;
    private ICharacterController _entityController;
    private IEntityStatsManager _statsManager;

    [Inject]
    private void Construct(ICharacterHealth health, IEntityStatsManager statsManager)
    {
        _health = health;
        _statsManager = statsManager;
    }

    public Transform Transform => transform;
    public Transform ProjectileSpawnPosition => _projectileSpawnPosition;
    public Collider2D Collider => _collider;
    public Rigidbody2D Rigidbody => _rigidbody;
    public Animator Animator => _animator;
    public EntityConfig Config => _config;
    public IAnimationEventReceiver AnimationEventReceiver => _animationEventReceiver;
    public IEntityStateMachine StateMachine => _stateMachine;

    /// <summary>
    /// If you need to access CharacterHealth specific methods, you can cast it like this: 
    /// (ICharacterHealth)Health or health = entity.Health as ICharacterHealth.
    /// </summary>
    public IEntityHealth Health => _health;
    public IEntityController EntityController => _entityController;
    public IEntityStatsManager StatsManager => _statsManager;

    public void Initialize()
    {
        SetComponents();

        _health.Initialize(this);

        _statsManager.Initialize(this);
    }

    public void SetController(IEntityController controller)
    {
        _entityController = (ICharacterController)controller;
    }

    public void SetConfig(EntityConfig config)
    {
        if (config is CharacterConfig characterConfig)
            _config = characterConfig;
        else
            Debug.LogError("Invalid config type for Character");
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

        if (_projectileSpawnPosition == null)
            _projectileSpawnPosition = GetComponentInChildren<ProjectileSpawnPoint>().transform;
    }
}
