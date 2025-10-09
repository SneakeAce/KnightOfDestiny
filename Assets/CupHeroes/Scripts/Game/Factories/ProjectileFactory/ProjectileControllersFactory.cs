using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectileControllersFactory : IProjectileControllersFactory
{
    private readonly List<ProjectileController> _projectileControllers = new();

    private IInstantiator _container;

    public ProjectileControllersFactory(IInstantiator container)
    {
        _container = container;
    }

    public List<ProjectileController> ProjectileControllers => _projectileControllers;

    public ProjectileController CreateProjectileController(IProjectile projectile, IEntity target)
    {
        ProjectileController controller = _container.Instantiate<ProjectileController>();

        if (controller == null)
        {
            Debug.LogError("Controller in EnemyControllersFactroy is null!");
            return null;
        }

        controller.SetTarget(target);

        controller.Initialize(projectile);

        if (_projectileControllers.Contains(controller) == false)
            _projectileControllers.Add(controller);

        return controller;
    }
}
