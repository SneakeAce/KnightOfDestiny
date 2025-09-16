using System;
using System.Collections;
using UnityEngine;

public abstract class BaseAttackStrategy : IAttackStrategy, IDisposable
{
    protected AttackState _state;

    public event Action OnAllTargetsDestroyed;

    public abstract IEnumerator AttackJob();
    public abstract void DealDamage();
    public abstract void SubscribingEvents();
    public abstract void UnsubscribingEvents();

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

    protected bool CheckDistanceToTarget()
    {
        float sqrDistance = (_state.Entity.Transform.position - _state.Target.Transform.position).sqrMagnitude;

        float sqrAttackRange = _state.AttackRange * _state.AttackRange;

        if (sqrDistance <= sqrAttackRange)
            return true;

        return false;
    }

    protected void OnEntityDestroyed(IEntity entity)
    {
        entity.Health.EntityDied -= OnEntityDestroyed;

        OnAllTargetsDestroyed?.Invoke();
    }
}
