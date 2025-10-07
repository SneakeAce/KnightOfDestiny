using System;
using System.Collections;
using UnityEngine;

public class AttackState : IEntityState, IDisposable
{
    private const float BaseAnimationSpeed = 1f;

    private IEntity _entity;

    private IEntityAttackStrategy _attackStrategy;

    private CoroutinePerformer _performer;
    private Coroutine _attackCoroutine;

    private AnimationClip _attackClip;

    private float _damage;
    private float _attacksPerSeconds;
    private float _delayBetweenAttack;
    private float _attackRange;
    private float _clipDuration;
    private float _remainingCooldown;

    private bool _canAttack = false;

    public IEntity Entity { get => _entity; }
    public float Damage { get => _damage; }
    public float AttacksPerSeconds { get => _attacksPerSeconds; }
    public float DelayBetweenAttack { get => _delayBetweenAttack; }
    public float AttackRange { get => _attackRange; }
    public float ClipDuration { get => _clipDuration; }
    public float RemainingCooldown { get => _remainingCooldown; }
    public bool CanAttack { get => _canAttack; }

    public AttackState(IEntity entity, CoroutinePerformer performer, IEntityAttackStrategy strategy)
    {
        _entity = entity;
        _performer = performer;
        _attackStrategy = strategy;
    }

    public void Dispose()
    {
        if (_attackStrategy != null)
            _attackStrategy.OnAllTargetsDestroyed -= Exit;
    }

    public void Enter()
    {
        _attackStrategy.Initialize(this);

        SetClipDuration();

        UpdateData();

        _attackStrategy.OnAllTargetsDestroyed += Exit;
        _attackStrategy.SubscribingEvents();

        _canAttack = true;

        RestartCoroutine(ref _attackCoroutine, _attackStrategy.AttackJob());
    }

    public void Exit()
    {
        if (_performer != null)
            RestartCoroutine(ref _attackCoroutine, _attackStrategy.AttackJob());

        _attackStrategy.OnAllTargetsDestroyed -= Exit;
        _attackStrategy.UnsubscribingEvents();

        _canAttack = false;
    }

    public void Update()
    {
        return;
    }

    public void UpdateData()
    {
        _damage = _entity.StatsManager.AttackStats.Damage;
        _attackRange = _entity.StatsManager.AttackStats.RangeAttackRange;

        _attacksPerSeconds = _entity.StatsManager.AttackStats.AttacksPerSecond;
        _delayBetweenAttack = BaseAnimationSpeed / _attacksPerSeconds;

        _remainingCooldown = Mathf.Max(0, _delayBetweenAttack - _clipDuration);

        SetAnimationSpeed();
    }

    public void SwitchStrategy(IEntityAttackStrategy strategy)
    {
        RestartCoroutine(ref _attackCoroutine, _attackStrategy.AttackJob());

        _attackStrategy.OnAllTargetsDestroyed -= Exit;
        _attackStrategy.UnsubscribingEvents();

        _canAttack = false;

        _attackStrategy = strategy;

        Enter();
    }

    public void RestartStrategy()
    {
        RestartCoroutine(ref _attackCoroutine, _attackStrategy.AttackJob());
    }

    private void SetAnimationSpeed()
    {
        if (_delayBetweenAttack < _clipDuration)
        {
            float animationSpeed = _clipDuration / _delayBetweenAttack;
            _entity.Animator.SetFloat("AttackMultiplierSpeed", animationSpeed);
        }
        else
        {
            _entity.Animator.SetFloat("AttackMultiplierSpeed", BaseAnimationSpeed);
        }
    }

    private void SetClipDuration()
    { 
        _attackClip = _entity.Config.AttackStats.AttackClip;
        _clipDuration = _attackClip.length;
    }

    private Coroutine RestartCoroutine(ref Coroutine routine, IEnumerator enumerator)
    {
        if (routine != null)
        {
            _performer.StopCoroutine(routine);
            routine = null;
        }

        routine = _performer.StartCoroutine(enumerator);

        return routine;
    }

}
