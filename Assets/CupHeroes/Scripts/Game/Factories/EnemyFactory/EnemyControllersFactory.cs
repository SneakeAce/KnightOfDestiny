using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyControllersFactory : IEnemyControllersFactory
{
    private readonly List<EnemyController> _enemyControllers = new();

    private IInstantiator _container;

    public EnemyControllersFactory(IInstantiator container)
    {
        _container = container;
    }

    public List<EnemyController> EnemyControllers => _enemyControllers;

    public EnemyController CreateEnemyController(IEnemy enemy)
    {
        EnemyController controller = _container.Instantiate<EnemyController>();

        if (controller == null)
        {
            Debug.LogError("Controller in EnemyControllersFactroy is null!");
            return null;
        }
        controller.Initialize(enemy);

        if (_enemyControllers.Contains(controller) == false)
            _enemyControllers.Add(controller);

        return controller;
    }
}
