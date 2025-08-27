using System;
using System.Collections.Generic;
using UnityEngine;

public class UIPoolsFactory : IPoolsFactory
{
    private IConfigsProvider _configsProvider;

    private UIPoolsConfig _poolsConfig;
    private UILibraryConfigs _uiLibraryConfigs;

    private PoolType _poolType;

    public UIPoolsFactory(IConfigsProvider configsProvider)
    {
        _configsProvider = configsProvider;

        _poolsConfig = (UIPoolsConfig)_configsProvider.GetPoolsConfig<UIPoolsConfig>();
        _uiLibraryConfigs = (UILibraryConfigs)_configsProvider.GetLibraryConfig<UILibraryConfigs>(); 
    }

    public PoolType PoolType => _poolType;

    public Dictionary<int, IObjectPool> CreatePools()
    {
        Debug.Log("UIPoolsFactory CreatePools");

        _poolType = _poolsConfig.PoolType;

        Dictionary<int, IObjectPool> tempDict = new();

        for (int i = 0; i < _uiLibraryConfigs.Configs.Count; i++)
        {
            UIPoolStats poolStats = _poolsConfig.PoolStats[i];

            UIConfig currentConfig = _uiLibraryConfigs.Configs[i];
            int currentType = currentConfig.GetTypeId();

            if (poolStats.Type != (UIElementType)currentType)
            {
                Debug.Log($"poolStats.{poolStats.Type} != currentType.{currentType}");
                continue;
            }

            IObjectPool pool = CreateObjectPool((UIElementType)currentType, poolStats, currentConfig.Prefab);

            if (tempDict.ContainsKey(currentType))
                continue;

            tempDict[currentType] = pool;
        }

        foreach (var kvp in tempDict) 
        {
            Debug.Log($"foreach in UIPoolsFactory. kvp.key = {kvp.Key}, kvp.Value = {kvp.Value}");
        }

        return tempDict;
    }

    private IObjectPool CreateObjectPool(Enum type, PoolStats poolStats, GameObject prefab)
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

        var pool = new ObjectPool<UIElement>(poolArgs);

        pool.CreatePool();

        if (pool == null)
            throw new ArgumentNullException("pool is null in EnemyPoolsFactory");

        return pool;
    }
}
