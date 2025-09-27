using System;
using System.Collections;
using UnityEngine;

public class RangeAttackByOnceTarget : BaseAttackStrategy
{
    private IEntity _target;

    public RangeAttackByOnceTarget(IEntity target)
    {
        _target = target;
    }

    public override event Action OnAllTargetsDestroyed;

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
        throw new NotImplementedException();
    }


}
