using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePoolsFactory : IPoolsFactory
{
    private ProjectileLibraryConfigs _libraryConfig;
    private ProjectilePoolsConfig _poolsConfig;

    private IConfigsProvider _configsProvider;

    private PoolType _poolType;

    public ProjectilePoolsFactory(IConfigsProvider configsProvider)
    {
        _configsProvider = configsProvider;

        _libraryConfig = (ProjectileLibraryConfigs)_configsProvider.GetLibraryConfig<ProjectileLibraryConfigs>();
        _poolsConfig = (ProjectilePoolsConfig)_configsProvider.GetPoolsConfig<ProjectilePoolsConfig>();
    }

    public PoolType PoolType => _poolType;

    public Dictionary<int, IObjectPool> CreatePools()
    {
        _poolType = _poolsConfig.PoolType;

        Dictionary<int, IObjectPool> tempDict = new();

        for (int i = 0; i < _libraryConfig.ProjectileConfigs.Count; i++)
        {
            ProjectilePoolStats poolStats = _poolsConfig.PoolsStats[i];

            ProjectileConfig currentConfig = _libraryConfig.ProjectileConfigs[i];
            ProjectileType currentProjectileType = currentConfig.ProjectileType;

            if (poolStats.ProjectileType != currentProjectileType)
            {
                Debug.Log($"poolStats.{poolStats.ProjectileType} != currentProjectileType.{currentProjectileType}");
                continue;
            }

            IObjectPool pool = CreatePool(currentProjectileType, poolStats, currentConfig.MainStats.Prefab);

            if (tempDict.ContainsKey((int)currentProjectileType))
                continue;

            tempDict[(int)currentProjectileType] = pool;
        }

        return tempDict;
    }

    private ObjectPool<Projectile> CreatePool(Enum type, PoolStats poolStats, GameObject prefab)
    {
        GameObject container = GameObject.Instantiate(
            poolStats.Container.gameObject,
            new Vector3(0f, 0f, 0f),
            Quaternion.identity);

        container.name = "Container " + type.ToString() + prefab.name;

        PoolCreatingArguments poolArgs = new PoolCreatingArguments(
            prefab,
            poolStats.MaxCountEntitiesInPool,
            poolStats.PoolCanExpand,
            container.transform
            );

        var pool = new ObjectPool<Projectile>(poolArgs);

        pool.CreatePool();

        if (pool == null)
            throw new ArgumentNullException("pool is null in EnemyPoolsFactory");

        return pool;
    }
}
