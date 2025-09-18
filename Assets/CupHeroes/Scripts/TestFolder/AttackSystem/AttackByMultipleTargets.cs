using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackByMultipleTargets : BaseAttackStrategy
{
    private List<IEntity> _targets = new();

    public override event Action OnAllTargetsDestroyed;

    public AttackByMultipleTargets(List<IEntity> targets)
    {
        _targets = targets;
    }

    public override void SubscribingEvents()
    {
        _state.Entity.AnimationEventReceiver.OnFrameAttack += DealDamage;

        _state.Entity.Health.EntityDied += OnEntityDestroyed;

        if (_targets.Count > 0)
        {
            for (int i = 0; i < _targets.Count; i++)
            {
                var target = _targets[i];

                if (target != null)
                    target.Health.EntityDied += OnEntityDestroyed;
            }
        }
    }

    public override void UnsubscribingEvents()
    {
        _state.Entity.AnimationEventReceiver.OnFrameAttack -= DealDamage;

        _state.Entity.Health.EntityDied -= OnEntityDestroyed;

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

    public override IEnumerator AttackJob()
    {
        while (_state.CanAttack && _targets.Count > 0)
        {
            _state.UpdateData();

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
