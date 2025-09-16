using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackByMultipleTargets : BaseAttackStrategy
{
    private List<IEntity> _targets = new();

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

    public override IEnumerator AttackJob()
    {
        throw new System.NotImplementedException();
    }

    public override void DealDamage()
    {
        throw new System.NotImplementedException();
    }

}
