using UnityEngine;
using Zenject;

public class Character : MonoBehaviour, IEntity
{
    private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private IAnimationEventReceiver _animationEventReceiver;
    private ICharacterHealth _health;
    private IEntityStateMachine _stateMachine;
    private IEntityController _entityController;
    private CharacterConfig _config;

    [Inject]
    private void Construct(ICharacterHealth health)
    {
        _health = health;
    }

    public Transform Transform => transform;
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


    public void Initialize()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _animationEventReceiver = GetComponent<AnimationEventReceiver>();

        _stateMachine = GetComponent<IEntityStateMachine>();

        _health.Initialize(this, _config.HealthStats.BaseValueHealth);
    }

    public void SetController(IEntityController controller)
    {
        _entityController = controller;
    }

    public void SetConfig(
        EntityConfig config)
    {
        if (config is CharacterConfig characterConfig)
            _config = characterConfig;
        else
            Debug.LogError("Invalid config type for Character");
    }
}
