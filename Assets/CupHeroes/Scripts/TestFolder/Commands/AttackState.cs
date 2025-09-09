using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AttackState : IEntityState, IDisposable
{
    private const float BaseAnimationSpeed = 1f;

    private IEntity _entity;
    private IEntity _target;

    private CoroutinePerformer _performer;
    private Coroutine _attackCoroutine;

    private AnimationClip _attackClip;

    private float _damage;
    private float _attacksPerSeconds;
    private float _delayBetweenAttack;
    private float _attackRange;
    private float _clipDuration;

    private bool _canAttack = false;

    public AttackState(IEntity entity, IEntity target, CoroutinePerformer performer)
    {
        _entity = entity;
        _target = target;
        _performer = performer;
    }

    public void Dispose()
    {
        UnsubsrubingEvents();
    }

    public void Enter()
    {
        SetParameters();

        SubsrubingEvents();

        _canAttack = true;
        _attackCoroutine = _performer.StartCoroutine(AttackJob());
    }

    public void Exit()
    {
        if (_attackCoroutine != null && _performer != null)
        {
            _performer.StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }

        UnsubsrubingEvents();

        _canAttack = false;
    }

    public void Update()
    {
        return;
    }

    private void SubsrubingEvents()
    {
        _entity.AnimationEventReceiver.OnFrameAttack += DamageDeal;

        _entity.Health.EntityDied += OnTargetDestroyed;
        _target.Health.EntityDied += OnTargetDestroyed;
    }    
    
    private void UnsubsrubingEvents()
    {
        if (_entity != null)
        {
            _entity.AnimationEventReceiver.OnFrameAttack -= DamageDeal;
            _entity.Health.EntityDied -= OnTargetDestroyed;
        }

        if (_target != null)
            _target.Health.EntityDied -= OnTargetDestroyed;
    }

    private void SetParameters()
    { 
        _entity.Config.AttackStats.ResetAttackSpeedToBase();

        _attackClip = _entity.Config.AttackStats.AttackClip;
        _clipDuration = _attackClip.length;
        _damage = _entity.Config.AttackStats.BaseDamage;
        _attackRange = _entity.Config.AttackStats.AttackRange;
    }

    private IEnumerator AttackJob()
    {
        while (_canAttack && _target != null)
        {
            SetAnimationSpeed();

            if (CheckDistanceToTarget() == false)
            {
                yield return null;
                continue;
            }

            _entity.Animator.SetTrigger("Attack");

            yield return new WaitForSeconds(_clipDuration);

            float remainingCooldown = _delayBetweenAttack - _attackClip.length;
            if (remainingCooldown > 0f)
                yield return new WaitForSeconds(remainingCooldown);

        }
    }

    private void SetAnimationSpeed()
    {
        _attacksPerSeconds = _entity.Config.AttackStats.CurrentAttacksPerSecond;

        _delayBetweenAttack = BaseAnimationSpeed / _attacksPerSeconds;

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

    private void DamageDeal()
    {
        DamageData data = new DamageData(_damage);

        _target.Health.TakeDamage(data);
    }

    private bool CheckDistanceToTarget()
    {
        float sqrDistance = (_entity.Transform.position - _target.Transform.position).sqrMagnitude;

        float sqrAttackRange = _attackRange * _attackRange;

        if (sqrDistance <= sqrAttackRange)
            return true;

        return false;
    }

    private void OnTargetDestroyed(IEntity entity)
    {
        _target.Health.EntityDied -= OnTargetDestroyed;

        UnsubsrubingEvents();

        Exit();

        _target = null;
    }
}
