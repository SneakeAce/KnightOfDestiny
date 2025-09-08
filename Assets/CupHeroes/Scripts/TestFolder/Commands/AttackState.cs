using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AttackState : IEntityState, IDisposable
{
    private const float MinDelayBeforeAttack = 0.001f;

    private IEntity _entity;
    private IEntity _target;

    private CoroutinePerformer _performer;
    private Coroutine _attackCoroutine;

    private AnimationClip _attackClip;

    private float _damage;
    private float _delayBetweenAttack;
    private float _attackRange;

    private float _baseAnimationSpeed = 1f;

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
        Debug.Log($"Enter AttackState at {_entity}");

        SetParameters();

        SubsrubingEvents();

        _canAttack = true;
        _attackCoroutine = _performer.StartCoroutine(AttackJob());
    }

    public void Exit()
    {
        Debug.Log($"Exit AttackState at {_entity}");

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
        _entity.AnimationEventReceiver.OnFrameAttack -= DamageDeal;

        _entity.Health.EntityDied -= OnTargetDestroyed;
        _target.Health.EntityDied -= OnTargetDestroyed;
    }

    private void SetParameters()
    { 
        _attackClip = _entity.Config.AttackStats.AttackClip;
        _damage = _entity.Config.AttackStats.BaseDamage;
        _delayBetweenAttack = _entity.Config.AttackStats.BaseAttackSpeed;
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

            yield return new WaitForSeconds(_delayBetweenAttack);
        }
    }

    private void SetAnimationSpeed()
    {
        _delayBetweenAttack = _entity.Config.AttackStats.BaseAttackSpeed;

        float clipDuration = _attackClip.length;
        float desiredDuration = _baseAnimationSpeed / Mathf.Max(MinDelayBeforeAttack, _delayBetweenAttack);
        float animationSpeed = clipDuration / desiredDuration;

        _entity.Animator.SetFloat("AttackMultiplierSpeed", animationSpeed);
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
        _target = null;

        UnsubsrubingEvents();

        Exit();
    }
}
