using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectileFactory : IProjectileFactory
{
    private DiContainer _container;
    private IPoolsManager _poolsManager;

    private IConfigsProvider _configsProvider;

    private List<ProjectileConfig> _projectileConfigs = new List<ProjectileConfig>();

    public ProjectileFactory(DiContainer container, IPoolsManager poolsManager, IConfigsProvider configsProvider)
    {
        _container = container;
        _poolsManager = poolsManager;
        _configsProvider = configsProvider;

        _projectileConfigs = _configsProvider.GetLibraryConfig<ProjectileLibraryConfigs>().GetConfigs<ProjectileConfig>();
    }

    public IProjectile CreateObject(ProjectileType type)
    {
        ProjectileConfig currentConfig = null;
        var currentType = type;

        foreach (var config in _projectileConfigs)
        {
            if (config.ProjectileType == currentType)
            {
                currentConfig = config;
                break;
            }
        }

        var pool = _poolsManager.GetPool<ProjectileType>(PoolType.ProjectilePool, currentType);

        if (pool == null)
            throw new ArgumentNullException($"{nameof(pool)} in {this.ToString()} is null!");

        Projectile projectile = (Projectile)pool.GetObjectFromPool();

        if (projectile == null)
            throw new ArgumentNullException($"{nameof(projectile)} in {this.ToString()} is null!");

        _container.Inject(projectile);

        projectile.SetConfig(currentConfig);

        projectile.Initialize();

        projectile.SetPool(pool);

        return projectile;
    }

}
