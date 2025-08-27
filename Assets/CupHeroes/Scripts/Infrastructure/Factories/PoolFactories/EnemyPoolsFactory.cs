using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolsFactory : IPoolsFactory
{
    private EnemyLibraryConfigs _libraryConfig;
    private EnemyPoolsConfig _poolsConfig;

    private IConfigsProvider _configsProvider;

    private PoolType _poolType;

    public EnemyPoolsFactory(IConfigsProvider configsProvider)
    {
        _configsProvider = configsProvider;

        _libraryConfig = (EnemyLibraryConfigs)_configsProvider.GetLibraryConfig<EnemyLibraryConfigs>();
        _poolsConfig = (EnemyPoolsConfig)_configsProvider.GetPoolsConfig<EnemyPoolsConfig>();
    }

    public PoolType PoolType => _poolType;

    public Dictionary<int, IObjectPool> CreatePools()
    {
        _poolType = _poolsConfig.PoolType;

        Dictionary<int, IObjectPool> tempDict = new();

        for (int i = 0; i < _libraryConfig.EnemyConfigs.Count; i++)
        {
            EnemyPoolStats poolStats = _poolsConfig.PoolsStats[i]; 

            EnemyConfig currentConfig = _libraryConfig.EnemyConfigs[i];
            int currentEntityType = (int)currentConfig.EnemyType;

            if (poolStats.Type != (EnemyType)currentEntityType)
            {
                Debug.Log($"poolStats.{poolStats.Type} != currentEntityType.{currentEntityType}");
                continue;
            }

            IObjectPool pool = CreatePool(poolStats, currentConfig.Prefab);

            tempDict[currentEntityType] = pool;
        }

        return tempDict;
    }

    private ObjectPool<Enemy> CreatePool(PoolStats poolStats, GameObject prefab)
    {
        GameObject container = GameObject.Instantiate(
            poolStats.Container.gameObject,
            new Vector3(0f, 0f, 0f),
            Quaternion.identity);

        PoolCreatingArguments poolArgs = new PoolCreatingArguments(
            prefab, 
            poolStats.MaxCountEntitiesInPool, 
            poolStats.PoolCanExpand,
            container.transform
            );

        var pool = new ObjectPool<Enemy>(poolArgs);

        pool.CreatePool();

        if (pool == null)
            throw new ArgumentNullException("pool is null in EnemyPoolsFactory");

        return pool;
    }

}
