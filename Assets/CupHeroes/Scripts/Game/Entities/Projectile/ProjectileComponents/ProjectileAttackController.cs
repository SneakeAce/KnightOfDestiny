using System;
using UnityEngine;

public class ProjectileAttackController : IDisposable
{
    private ProjectileController _controller;

    private IProjectile _projectile;

    private IEntity _target;
    private IEntity _parent;

    private CoroutinePerformer _performer;
    private Coroutine _attackCoroutine;

    private IProjectileAttackStrategy _strategy;
    private ProjectileAttackData _data;

    public ProjectileAttackController(ProjectileController controller, IProjectile projectile, IEntity parent, CoroutinePerformer performer)
    {
        _controller = controller;
        _projectile = projectile;
        _parent = parent;
        _performer = performer;
    }

    public IEntity Parent => _parent;
    public IProjectile Projectile => _projectile;

    public event Action<IProjectile> OnAttackProjectileDestroyed;

    public void Dispose()
    {
        StopAttackCoroutine();

        _strategy.ProjectileCollided -= ProjectileDestroyed;
        _strategy.Dispose();
    }

    public void Initialize()
    {
        StartAttackStrategy();
    }

    public void SetTarget(IEntity target)
    {
        _target = target;
    }

    public void ProjectileDestroyed()
    {
        OnAttackProjectileDestroyed?.Invoke(_projectile);
    }

    private void StartAttackStrategy()
    {
        _strategy = GetAttackStrategy();

        _strategy.ProjectileCollided += ProjectileDestroyed;

        _strategy.Initialize(_data);

        _attackCoroutine = _performer.StartCoroutine(_strategy.AttackJob());
    }

    private IProjectileAttackStrategy GetAttackStrategy()
    {
        IProjectileAttackStrategy strategy = null;

        if (_projectile.ProjectileConfig.MainStats.IsSplashAttack)
        {
            strategy = new ProjectileAttackByOnceTarget(_target);

            _data = new ProjectileAttackData(
                this,
                _parent.StatsManager.AttackStats.Damage,
                _projectile.ProjectileConfig.MainStats.Speed,
                _projectile.ProjectileConfig.MainStats.IsSplashAttack,
                _projectile.ProjectileConfig.MainStats.SplashRadius
                );
        }
        else
        {
            strategy = new ProjectileAttackByOnceTarget(_target);

            _data = new ProjectileAttackData(
                this,
                _parent.StatsManager.AttackStats.Damage,
                _projectile.ProjectileConfig.MainStats.Speed,
                _projectile.ProjectileConfig.MainStats.IsSplashAttack,
                _projectile.ProjectileConfig.MainStats.SplashRadius
                );
        }

        return strategy;
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
