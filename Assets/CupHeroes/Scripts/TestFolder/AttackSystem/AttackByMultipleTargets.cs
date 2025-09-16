using System.Collections;
using UnityEngine;

public class AttackByMultipleTargets : BaseAttackStrategy
{
    public override void SubscribingEvents()
    {
        _state.Entity.AnimationEventReceiver.OnFrameAttack += DealDamage;

        _state.Entity.Health.EntityDied += OnEntityDestroyed;

        if (_state.Targets.Count > 0)
        {
            for (int i = 0; i < _state.Targets.Count; i++)
            {
                var target = _state.Targets[i];

                if (target != null)
                    target.Health.EntityDied += OnEntityDestroyed;
            }
        }
    }

    public override void UnsubscribingEvents()
    {
        _state.Entity.AnimationEventReceiver.OnFrameAttack += DealDamage;

        _state.Entity.Health.EntityDied += OnEntityDestroyed;

        if (_state.Targets.Count > 0)
        {
            for (int i = 0; i < _state.Targets.Count; i++)
            {
                var target = _state.Targets[i];

                if (target != null)
                    target.Health.EntityDied += OnEntityDestroyed;
            }
        }
    }

    public override IEnumerator AttackJob()
    {
        throw new System.NotImplementedException();
    }

    public override void DealDamage()
    {
        throw new System.NotImplementedException();
    }

}
