using System;
using System.Collections;
using UnityEngine;

public abstract class BaseAttackStrategy : IAttackStrategy
{
    protected AttackState _state;

    public abstract event Action OnAllTargetsDestroyed;

    public abstract IEnumerator AttackJob();
    public abstract void DealDamage();
    public abstract void SubscribingEvents();
    public abstract void UnsubscribingEvents();
    public abstract void OnEntityDestroyed(IEntity entity);

    public void Dispose()
    {
        UnsubscribingEvents();
    }

    public void Initialize(AttackState state)
    {
        _state = state;
    }

    protected void DamageDeal(IEntity target)
    {
        DamageData data = new DamageData(_state.Damage);

        target.Health.TakeDamage(data);
    }

    protected bool CheckDistanceToTarget(IEntity target)
    {
        float sqrDistance = (_state.Entity.Transform.position - target.Transform.position).sqrMagnitude;

        float sqrAttackRange = _state.AttackRange * _state.AttackRange;

        if (sqrDistance <= sqrAttackRange)
            return true;

        return false;
    }
}
