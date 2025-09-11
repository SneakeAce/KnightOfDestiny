using System;
using Zenject;

public class TickUpdater : ITickable, IDisposable
{
    private readonly IEnemyControllersFactory _enemyControllersFactory;

    private bool _isEnable = false;

    public TickUpdater(IEnemyControllersFactory enemyControllersFactory)
    {
        _enemyControllersFactory = enemyControllersFactory;
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

        UpdateControllers();
    }

    private void UpdateControllers()
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
