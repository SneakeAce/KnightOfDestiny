using System;
using Zenject;

/// <summary>
/// TickUpdater called Tick() at all classes which realized ITickable interface.
/// Case: EnemyController
/// </summary>
public class TickUpdater : ITickable, IDisposable
{
    private readonly IEnemyControllersFactory _enemyControllersFactory;
    //private readonly IProjectileControllersFactory _projectileControllersFactory;

    private bool _isEnable = false;

    public TickUpdater(IEnemyControllersFactory enemyControllersFactory)
    {
        _enemyControllersFactory = enemyControllersFactory;
        //_projectileControllersFactory = projectileControllersFactory;
    }

    public void Dispose()
    {
        _isEnable = false;
    }

    public void Initialize() => _isEnable = true;

    public void Tick()
    {
        if (_isEnable == false)
            return;

        UpdateEnemyControllers();
        //UpdateProjectileControllers();
    }

    //private void UpdateProjectileControllers()
    //{
    //    for (int i = _projectileControllersFactory.ProjectileControllers.Count - 1; i >= 0; i--)
    //    {
    //        var controller = _projectileControllersFactory.ProjectileControllers[i];

    //        if (controller == null)
    //        {
    //            _projectileControllersFactory.ProjectileControllers.RemoveAt(i);
    //            continue;
    //        }

    //        controller.Tick();
    //    }
    //}

    private void UpdateEnemyControllers()
    {
        for (int i = _enemyControllersFactory.EnemyControllers.Count - 1; i >= 0; i--)
        {
            var controller = _enemyControllersFactory.EnemyControllers[i];

            if (controller == null)
            {
                _enemyControllersFactory.EnemyControllers.RemoveAt(i);
                continue;
            }

            controller.Tick();
        }
    }
}
