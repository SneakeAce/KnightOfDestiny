using System;
using System.Collections;

public struct ProjectileAttackData
{
    public ProjectileAttackData(ProjectileAttackController controller, float damage, 
        float speed, bool isSplashAttack, float splashRadius)
    {
        Controller = controller;
        Damage = damage;
        Speed = speed;
        IsSplashAttack = isSplashAttack;
        SplashRadius = splashRadius;
    }

    public ProjectileAttackController Controller { get; }
    public float Damage { get; }
    public float Speed { get; }
    public bool IsSplashAttack { get; }
    public float SplashRadius { get; }
}

public abstract class ProjectileAttackBase : IProjectileAttackStrategy
{
    protected ProjectileAttackController _controller;
    protected ProjectileAttackData _attackData;

    protected bool _wasCollision;

    public abstract event Action OnAllTargetsDestroyed;
    public event Action ProjectileCollided;

    public abstract void SubscribingEvents();
    public abstract void UnsubscribingEvents();
    public abstract IEnumerator AttackJob();
    public abstract void DealDamage();
    public abstract void OnEntityDestroyed(IEntity entity);

    public void Dispose()
    {
        UnsubscribingEvents();

        _wasCollision = false;
    }

    public void Initialize(ProjectileAttackData data)
    {
        _controller = data.Controller;

        _wasCollision = false;

        _attackData = data;
    }

    protected void DamageDeal(IEntity target)
    {
        DamageData damageData = new DamageData(_attackData.Damage);

        target.Health.TakeDamage(damageData);
    }

    protected void ProjectileDestroyed()
    {
        ProjectileCollided?.Invoke();
    }
}
