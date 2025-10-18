using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCardPoolsFactory : IPoolsFactory
{
    private UpgradeCardLibraryConfigs _libraryConfigs;
    private UpgradeCardPoolsConfig _poolsConfig;

    private PoolType _poolType;

    private IConfigsProvider _configsProvider;

    public UpgradeCardPoolsFactory(IConfigsProvider configsProvider)
    {
        _configsProvider = configsProvider;

        _libraryConfigs = (UpgradeCardLibraryConfigs)_configsProvider.GetLibraryConfig<UpgradeCardLibraryConfigs>();
        _poolsConfig = (UpgradeCardPoolsConfig)_configsProvider.GetPoolsConfig<UpgradeCardPoolsConfig>();
    }

    public PoolType PoolType => _poolType;

    public Dictionary<int, IObjectPool> CreatePools()
    {
        _poolType = _poolsConfig.PoolType;

        Dictionary<int, IObjectPool> tempDict = new();

        for (int i = 0; i < _libraryConfigs.UpgradeCardConfigs.Count; i++)
        {
            UpgradeCardPoolStats poolStats = _poolsConfig.PoolsStats[i];

            UpgradeCardConfig currentConfig = _libraryConfigs.UpgradeCardConfigs[i];
            UpgradeType currentUpgradeType = currentConfig.UpgradeType;

            if (poolStats.Type != currentUpgradeType)
            {
                Debug.Log($"poolStats.{poolStats.Type} != currentUpgradeType.{currentUpgradeType}");
                continue;
            }

            IObjectPool pool = CreatePool(currentUpgradeType, poolStats, currentConfig.Prefab);

            if (tempDict.ContainsKey((int)currentUpgradeType))
                continue;

            tempDict[(int)currentUpgradeType] = pool;
        }

        return tempDict;
    }

    private ObjectPool<UpgradeCard> CreatePool(Enum type, PoolStats poolStats, GameObject prefab)
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

        var pool = new ObjectPool<UpgradeCard>(poolArgs);

        pool.CreatePool();

        if (pool == null)
            throw new ArgumentNullException("pool is null in EnemyPoolsFactory");

        return pool;
    }
}
