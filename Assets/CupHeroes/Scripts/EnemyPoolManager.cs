using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : IEnemyPoolManager
{
    private Dictionary<EntityAttackType, ObjectPool<Enemy>> _enemiesPools = new();
    
    private Dictionary<ObjectPool<Enemy>, EnemyConfig> _enemyConfigs = new();

    private EnemyLibraryConfigs _enemyLibraryConfigs;
    private EnemyPoolsConfig _enemyPoolsConfig;
    private IEnemyPoolsFactory _enemyPoolsFactory;

    public EnemyPoolManager(EnemyLibraryConfigs enemyLibraryConfigs, IEnemyPoolsFactory enemyPoolsFactory, EnemyPoolsConfig enemyPoolsConfig)
    {
        _enemyLibraryConfigs = enemyLibraryConfigs;

        _enemyPoolsFactory = enemyPoolsFactory;

        _enemyPoolsConfig = enemyPoolsConfig;
    }

    public void Initialize()
    {
        CreatePools();
        Debug.Log($"EnemyPoolManager initialized with {_enemiesPools.Count} pools");
    }

    public ObjectPool<Enemy> GetPool(EntityAttackType attackType)
    {
        if (_enemiesPools.TryGetValue(attackType, out var pool))
        {
            return pool;
        }

        Debug.LogWarning($"No pool found for attack type: {attackType}");
        return null;
    }

    public ObjectPool<Enemy> GetRandomPool()
    {
        if (_enemiesPools.Count == 0)
        {
            Debug.LogWarning("No enemy pools available");
            return null;
        }

        var randomIndex = Random.Range(0, _enemiesPools.Count);
        var enumerator = _enemiesPools.Values.GetEnumerator();

        for (int i = 0; i <= randomIndex; i++)
        {
            enumerator.MoveNext();
        }

        return enumerator.Current;
    }

    public EnemyConfig GetConfig(ObjectPool<Enemy> pool)
    {
        if (_enemyConfigs.TryGetValue(pool, out var config))
        {
            return config;
        }

        Debug.LogWarning("No config found for the specified pool");
        return null;
    }

    private void CreatePools()
    {
        for (int i = 0; i < _enemyLibraryConfigs.EnemyConfigs.Count; i++)
        {
            var config = _enemyLibraryConfigs.EnemyConfigs[i];
            var pool = _enemyPoolsFactory.CreatePool(_enemyPoolsConfig.Pools[i]);

            if (pool == null)
            {
                Debug.LogWarning($"Failed to create pool for enemy: {config.name}");
                continue;
            }
            
            if (_enemiesPools.ContainsKey(config.MainStats.AttackType))
            {
                Debug.LogWarning($"Pool for attack type {config.MainStats.AttackType} already exists. Overwriting.");
            }

            _enemiesPools[config.MainStats.AttackType] = pool;
            _enemyConfigs[pool] = config;
        }
    }
}