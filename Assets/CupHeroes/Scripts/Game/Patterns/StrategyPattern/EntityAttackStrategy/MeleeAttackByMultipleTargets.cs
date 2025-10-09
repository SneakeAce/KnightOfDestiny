using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeleeAttackByMultipleTargets : BaseAttackStrategy
{
    private List<IEntity> _targets = new();
    private IEntity _closestTarget;

    public override event Action OnAllTargetsDestroyed;

    public MeleeAttackByMultipleTargets()
    {
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

        if (_targets.Count > 0)
        {
            for (int i = 0; i < _targets.Count; i++)
            {
                var target = _targets[i];

                if (target != null)
                    target.Health.EntityDied -= OnEntityDestroyed;
            }

            _targets.Clear();
        }
    }
    
    public override void OnEntityDestroyed(IEntity entity)
    {
        if (entity is ICharacter)
        {
            entity.Health.EntityDied -= OnEntityDestroyed;

            _targets.Clear();

            OnAllTargetsDestroyed?.Invoke();

            return;
        }

        entity.Health.EntityDied -= OnEntityDestroyed;

        _targets.Remove(entity);

        if (_targets.Count == 0)
            OnAllTargetsDestroyed?.Invoke();
    }

    public override void SwitchTarget(IEntity newTarget)
    {
        if (_closestTarget != null)
        {
            _closestTarget.Health.EntityDied -= OnEntityDestroyed;

            _targets.Remove(_closestTarget);
        }

        _closestTarget = newTarget;

        if (_closestTarget != null)
        {
            _closestTarget.Health.EntityDied += OnEntityDestroyed;

            _targets.Add(_closestTarget);
        }
    }

    public override void GetTargets(List<IEntity> targets)
    {
        if (_targets.Count > 0)
        {
            for (int i = 0; i < _targets.Count; i++)
            {
                var target = _targets[i];

                if (target != null)
                    target.Health.EntityDied -= OnEntityDestroyed;
            }
        }

        _targets.Clear();

        _targets.AddRange(targets);

        if (_targets.Count > 0)
        {
            for (int i = 0; i < _targets.Count; i++)
            {
                var target = _targets[i];

                if (target != null)
                    target.Health.EntityDied += OnEntityDestroyed;
            }
        }

        _closestTarget = _targets.FirstOrDefault();
    }

    public override IEnumerator AttackJob()
    {
        while (_state.CanAttack)
        {
            _state.UpdateData();

            if (_closestTarget == null)
            {
                yield return null;
                continue;
            }

            if (_closestTarget != null)
                _state.Entity.Animator.SetTrigger("Attack");

            yield return new WaitForSeconds(_state.ClipDuration);

            if (_state.RemainingCooldown > 0f)
                yield return new WaitForSeconds(_state.RemainingCooldown);
        }
    }

    public override void DealDamage()
    {
        for (int i = 0; i < _targets.Count; i++)
        {
            var target = _targets[i];

            if (CheckDistanceToTarget(target) == false)
            {
                continue;
            }

            DamageDeal(target);
        }
    }
}
