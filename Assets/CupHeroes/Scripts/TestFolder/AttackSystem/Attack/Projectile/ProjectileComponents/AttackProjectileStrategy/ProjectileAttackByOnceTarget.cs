using System;
using System.Collections;

public class ProjectileAttackByOnceTarget : ProjectileAttackBase
{
    private IEntity _target;

    public ProjectileAttackByOnceTarget(IEntity target)
    {
        _target = target;
    }

    public override event Action OnAllTargetsDestroyed;

    public override void OnEntityDestroyed(IEntity entity)
    {
        entity.Health.EntityDied -= OnEntityDestroyed;

        OnAllTargetsDestroyed?.Invoke();
    }

    public override void SubscribingEvents()
    {
        _controller.Parent.Health.EntityDied += OnEntityDestroyed;
        _target.Health.EntityDied += OnEntityDestroyed;
    }

    public override void UnsubscribingEvents()
    {
        _controller.Parent.Health.EntityDied -= OnEntityDestroyed;
        _target.Health.EntityDied -= OnEntityDestroyed;
    }

    public override IEnumerator AttackJob()
    {
        while (_controller.Projectile != null && _wasCollision == false)
        {
            if (_target == null)
            {
                yield return null;
                continue;
            }

            if (_controller.Projectile.Collider.IsTouching(_target.Collider))
            {
                DealDamage();

                _wasCollision = true;

                ProjectileDestroyed();
            }

            yield return null;
        }
    }

    public override void DealDamage()
    {
        DamageDeal(_target);
    }

}
