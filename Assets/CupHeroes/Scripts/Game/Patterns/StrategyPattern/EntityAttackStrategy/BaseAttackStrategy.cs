using System;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseAttackStrategy : IEntityAttackStrategy
{
    protected AttackState _state;

    public abstract event Action OnAllTargetsDestroyed;

    public abstract IEnumerator AttackJob();
    public abstract void SubscribingEvents();
    public abstract void UnsubscribingEvents();
    public abstract void OnEntityDestroyed(IEntity entity);
    public abstract void SwitchTarget(IEntity newTarget);
    public abstract void GetTargets(List<IEntity> targets);
    public abstract void DealDamage();

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
        if (target == null)
            return false;

        float sqrDistance = (_state.Entity.Transform.position - target.Transform.position).sqrMagnitude;

        float sqrAttackRange = _state.AttackRange * _state.AttackRange;

        if (sqrDistance <= sqrAttackRange)
            return true;

        return false;
    }

}
