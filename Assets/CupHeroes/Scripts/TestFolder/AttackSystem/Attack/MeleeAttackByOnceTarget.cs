using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeleeAttackByOnceTarget : BaseAttackStrategy
{
    private IEntity _target;

    public override event Action OnAllTargetsDestroyed;

    public MeleeAttackByOnceTarget()
    {
    }

    public MeleeAttackByOnceTarget(IEntity target)
    {
        _target = target;
    }

    public override void SubscribingEvents()
    {
        if (_state.Entity != null)
        {
            _state.Entity.AnimationEventReceiver.OnFrameAttack += DealDamage;
            _state.Entity.Health.EntityDied += OnEntityDestroyed;
        }
    }

    public override void UnsubscribingEvents()
    {
        if (_state.Entity != null)
        {
            _state.Entity.AnimationEventReceiver.OnFrameAttack -= DealDamage;
            _state.Entity.Health.EntityDied -= OnEntityDestroyed;
        }

        if (_target != null)
            _target.Health.EntityDied -= OnEntityDestroyed;
    }

    public override void OnEntityDestroyed(IEntity entity)
    {
        entity.Health.EntityDied -= OnEntityDestroyed;

        OnAllTargetsDestroyed?.Invoke();
    }

    public override void SwitchTarget(IEntity newTarget)
    {
        if (_target != null)
            _target.Health.EntityDied -= OnEntityDestroyed;

        _target = newTarget;

        if (_target != null)
            _target.Health.EntityDied += OnEntityDestroyed;

        Debug.Log($"{_state.Entity} - {this.ToString()} - SwitchTarget - _target = {_target}");
    }

    public override void GetTargets(List<IEntity> targets)
    {
        _target = targets.FirstOrDefault();

        Debug.Log($"{_state.Entity} - {this.ToString()} - GetTargets - _target = {_target}");
        
        if (_target != null)
            _target.Health.EntityDied += OnEntityDestroyed; 
    }

    public override IEnumerator AttackJob()
    {
        while (_state.CanAttack)
        {
            _state.UpdateData();

            if (_target == null || CheckDistanceToTarget(_target) == false)
            {
                yield return null;
                continue;
            }

            _state.Entity.Animator.SetTrigger("Attack");

            yield return new WaitForSeconds(_state.ClipDuration);

            if (_state.RemainingCooldown > 0f)
                yield return new WaitForSeconds(_state.RemainingCooldown);
        }
    }

    public override void DealDamage()
    {
        DamageDeal(_target);
    }

}
