using System.Collections;
using UnityEngine;

public class AttackByOnceTarget : BaseAttackStrategy
{
    public override void SubscribingEvents()
    {
        _state.Entity.AnimationEventReceiver.OnFrameAttack += DealDamage;

        _state.Entity.Health.EntityDied += OnEntityDestroyed;
        _state.Target.Health.EntityDied += OnEntityDestroyed;
    }

    public override void UnsubscribingEvents()
    {
        _state.Entity.AnimationEventReceiver.OnFrameAttack -= DealDamage;

        _state.Entity.Health.EntityDied -= OnEntityDestroyed;
        _state.Target.Health.EntityDied -= OnEntityDestroyed;
    }

    public override IEnumerator AttackJob()
    {
        while (_state.CanAttack && _state.Target != null)
        {
            _state.UpdateData();

            if (CheckDistanceToTarget() == false)
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
        DamageDeal(_state.Target);
    }

}
