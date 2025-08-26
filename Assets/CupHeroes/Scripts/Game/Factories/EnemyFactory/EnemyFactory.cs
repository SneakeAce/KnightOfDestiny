using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class EnemyFactory : IEnemyFactory
{
    private DiContainer _container;
    private IPoolsManager _poolsManager;

    private IConfigsProvider _configsProvider;
    private List<EnemyConfig> _enemyConfigs;

    private List<EnemyType> _availableEnemiesType = new();

    public EnemyFactory(DiContainer container, IPoolsManager poolsManager, IConfigsProvider configsProvider)
    {
        _container = container;
        _poolsManager = poolsManager;
        _configsProvider = configsProvider;

        _enemyConfigs = _configsProvider.GetLibraryConfig<EnemyLibraryConfigs>().GetConfigs<EnemyConfig>();

        _availableEnemiesType = Enum.GetValues(typeof(EnemyType))
            .Cast<EnemyType>()
            .Where(type => type != EnemyType.None)
            .ToList();
    }

    public IEnemy CreateEnemy()
    {
        EnemyConfig currentConfig = null;

        var currentEnemyType = _availableEnemiesType[UnityEngine.Random.Range(0, _availableEnemiesType.Count)];

        foreach (var config in _enemyConfigs)
        {
            if (config.EnemyType == currentEnemyType)
            {
                currentConfig = config;
                break;
            }
            else
            {
                continue;
            }
        }

        var pool = _poolsManager.GetPool<EnemyType>(PoolType.EnemyEntityPool, currentEnemyType);

        Debug.Log($"pool in EnemyFactory = {pool}");

        Enemy enemy = (Enemy)pool.GetObjectFromPool();

        Debug.Log($"enemy in EnemyFactory = {enemy}");

        if (enemy == null)
            return null;

        _container.Inject(enemy);

        enemy.SetConfig(currentConfig);
        enemy.Initialize();
        enemy.SetPool(pool);

        return enemy;
    }
}
