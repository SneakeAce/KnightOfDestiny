using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolsManager : IPoolsManager
{
    private Dictionary<PoolType, Dictionary<int, IObjectPool>> _availablePools = new();

    private List<IPoolsFactory> _poolsFactories;

    public PoolsManager(List<IPoolsFactory> poolsFactories)
    {
        _poolsFactories = poolsFactories;
    }

    public void Initialize() => CreatePools();

    public IObjectPool GetPool<TEnum>(PoolType poolType, TEnum type)
        where TEnum : Enum
    {
        if (_availablePools.TryGetValue(poolType, out var dictionaryPools))
        {
            int key = Convert.ToInt32(type);

            if (dictionaryPools.TryGetValue(key, out var pool))
                return pool;

            Debug.LogWarning($"This pool not found!");
            return null;
        }
        else
        {
            Debug.LogWarning($"This {poolType} is not in pool! ");
            return null;
        }

        throw new KeyNotFoundException($"Pool not found: {poolType} -> {type}");
    }

    private void CreatePools()
    {
        for (int i = 0; i < _poolsFactories.Count; i++)
        {
            Dictionary<int, IObjectPool> pools = _poolsFactories[i].CreatePools();

            _availablePools.Add(_poolsFactories[i].PoolType, pools);
        }
    }
}
