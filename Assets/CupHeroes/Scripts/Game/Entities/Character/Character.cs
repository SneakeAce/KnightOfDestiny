using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class Character : MonoBehaviour, IEntity
{
    private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private IEntityHealth _health;
    private IEntityStateMachine _stateMachine;

    private CharacterConfig _config;

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

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _stateMachine = GetComponent<IEntityStateMachine>();

        _health.Initialize(this, _config.MainStats.BaseValueHealth);

        StartCoroutine(TakeDamage());
    }

    public void SetConfig(EntityConfig config)
    {
        if (config is CharacterConfig characterConfig)
            _config = characterConfig;
        else
            Debug.LogError("Invalid config type for Character");
    }

    private IEnumerator TakeDamage()
    {
        Debug.Log("TakeDamage Coroutine start");
        yield return new WaitForSeconds(20f);

        float dm = 10f;
        DamageData d = new DamageData(dm);

        _health.TakeDamage(d);

        yield return new WaitForSeconds(30f);

        _health.TakeDamage(d);
    }
}
