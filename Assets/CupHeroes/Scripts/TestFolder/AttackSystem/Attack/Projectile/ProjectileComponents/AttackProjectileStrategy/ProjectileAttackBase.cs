using System;
using System.Collections;

public struct ProjectileAttackData
{
    public ProjectileAttackData(float damage, float speed, bool isSplashAttack, float splashRadius)
    {
        Damage = damage;
        Speed = speed;
        IsSplashAttack = isSplashAttack;
        SplashRadius = splashRadius;
    }

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
        _wasCollision = false;

        _attackData = data;

        _controller.OnProjectileDestroyed += ProjectileDestroyed;
    }

    protected void DamageDeal(IEntity target)
    {
        DamageData damageData = new DamageData(_attackData.Damage);

        target.Health.TakeDamage(damageData);
    }

    private void ProjectileDestroyed(IProjectile projectile)
    {
        _controller.OnProjectileDestroyed -= ProjectileDestroyed;

        // Ќе уверен, что нужно тут писать, потом изменить!
        Dispose();
    }
}
