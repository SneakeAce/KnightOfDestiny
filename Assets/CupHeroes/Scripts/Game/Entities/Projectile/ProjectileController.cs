using System;
using Zenject;

public class ProjectileController : ITickable, IDisposable
{
    private IEntity _target;
    private IEntity _parent;

    private IProjectile _projectile;

    private CoroutinePerformer _performer;

    private ProjectileAttackController _attackController;
    private ProjectileMoveController _moveController;

    public ProjectileController(CoroutinePerformer performer)
    {
        _performer = performer;
    }

    public event Action<IProjectile> ProjectileDestroyed;

    public void Dispose()
    {
        if (_moveController != null)
            _moveController.OnEndPoint -= OnProjectileDestroyed;

        if (_attackController != null)
            _attackController.OnAttackProjectileDestroyed -= OnProjectileDestroyed;

        _attackController?.Dispose();
        _moveController?.Dispose();

        _attackController = null;
        _moveController = null;
        _projectile = null;
    }

    public void Tick()
    {
        return;
    }

    public void Initialize(IProjectile projectile)
    {
        _projectile = projectile;
        _parent = _projectile.Parent;

        InitializeComponents();
    }

    public void SetTarget(IEntity target)
    {
        _target = target;
    }

    private void InitializeComponents()
    {
        if (_moveController == null)
        {
            _moveController = new ProjectileMoveController(_projectile, _target, _performer);
            _moveController.OnEndPoint += OnProjectileDestroyed;
        }

        if (_attackController == null)
        {
            _attackController = new ProjectileAttackController(this, _projectile, _parent, _performer);
            _attackController.OnAttackProjectileDestroyed += OnProjectileDestroyed;

            _attackController.SetTarget(_target);
        }

        _attackController.Initialize();
        _moveController.Initialize();
    }

    private void OnProjectileDestroyed(IProjectile projectile)
    {
        ProjectileDestroyed?.Invoke(projectile);

        if (_moveController != null)
            _moveController.OnEndPoint -= OnProjectileDestroyed;

        if (_attackController != null)
            _attackController.OnAttackProjectileDestroyed -= OnProjectileDestroyed;
     
        _attackController?.Dispose();
        _moveController?.Dispose();

        _attackController = null;
        _moveController = null;
        _projectile = null;
    }

}
