using System;
using UnityEngine;

public class ProjectileAttackController : IDisposable
{
    private IProjectile _projectile;

    private IEntity _target;
    private IEntity _parent;

    private CoroutinePerformer _performer;
    private Coroutine _attackCoroutine;

    private IProjectileAttackStrategy _strategy;
    private ProjectileAttackData _data;

    public ProjectileAttackController(IProjectile projectile, IEntity parent, CoroutinePerformer performer)
    {
        _projectile = projectile;
        _parent = parent;
        _performer = performer;
    }

    public IEntity Parent => _parent;
    public IProjectile Projectile => _projectile;

    public event Action<IProjectile> OnProjectileDestroyed;

    public void Dispose()
    {
        StopAttackCoroutine();
    }

    public void Initialize()
    {
        StartAttackStrategy();
    }

    public void SetTarget(IEntity target)
    {
        _target = target;
    }

    private void StartAttackStrategy()
    {
        _strategy = GetAttackStrategy();

        _strategy.Initialize(_data);

        _attackCoroutine = _performer.StartCoroutine(_strategy.AttackJob());
    }

    private IProjectileAttackStrategy GetAttackStrategy()
    {
        IProjectileAttackStrategy strategy = null;

        if (_parent.Config.AttackStats.ProjectileStats.IsSplashAttack)
        {
            strategy = new ProjectileAttackByOnceTarget(_target);

            _data = new ProjectileAttackData(
                _parent.StatsManager.AttackStats.Damage,
                _parent.Config.AttackStats.ProjectileStats.Speed,
                _parent.Config.AttackStats.ProjectileStats.IsSplashAttack,
                _parent.Config.AttackStats.ProjectileStats.SplashRadius
                );
        }
        else
        {
            strategy = new ProjectileAttackByOnceTarget(_target);

            _data = new ProjectileAttackData(
                _parent.StatsManager.AttackStats.Damage,
                _parent.Config.AttackStats.ProjectileStats.Speed,
                _parent.Config.AttackStats.ProjectileStats.IsSplashAttack,
                _parent.Config.AttackStats.ProjectileStats.SplashRadius
                );
        }

        return strategy;
    }

    private void ProjectileDestroyed(IProjectile projectile)
    {
        StopAttackCoroutine();

        _strategy.Dispose();
    }

    private void StopAttackCoroutine()
    {
        if (_attackCoroutine != null)
        {
            _performer.StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
    }

}
