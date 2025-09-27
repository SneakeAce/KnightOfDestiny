using System;
using System.Collections;
using UnityEngine;

public class MeleeAttackByOnceTarget : BaseAttackStrategy
{
    private IEntity _target;

    public override event Action OnAllTargetsDestroyed;

    public MeleeAttackByOnceTarget(IEntity target)
    {
        _target = target;
    }

    public override void SubscribingEvents()
    {
        _state.Entity.AnimationEventReceiver.OnFrameAttack += DealDamage;

        _state.Entity.Health.EntityDied += OnEntityDestroyed;
        _target.Health.EntityDied += OnEntityDestroyed;
    }

    public override void UnsubscribingEvents()
    {
        _state.Entity.AnimationEventReceiver.OnFrameAttack -= DealDamage;

        _state.Entity.Health.EntityDied -= OnEntityDestroyed;
        _target.Health.EntityDied -= OnEntityDestroyed;
    }

    public override void OnEntityDestroyed(IEntity entity)
    {
        entity.Health.EntityDied -= OnEntityDestroyed;

        OnAllTargetsDestroyed?.Invoke();
    }

    public override IEnumerator AttackJob()
    {
        while (_state.CanAttack && _target != null)
        {
            _state.UpdateData();

            if (CheckDistanceToTarget(_target) == false)
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
