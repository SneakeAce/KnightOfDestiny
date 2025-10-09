using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        }

        var pool = _poolsManager.GetPool<EnemyType>(PoolType.EnemyEntityPool, currentEnemyType);

        if (pool == null)
        {
            UnityEngine.Debug.Log($"{nameof(pool)} in {this.ToString()} is null!");
            return null;
        }

        Enemy enemy = (Enemy)pool.GetObjectFromPool();

        if (enemy == null)
        {
            UnityEngine.Debug.Log($"{this.ToString()} enemy is null." +
                $" Most likely, there were not enough objects in the spawn pool");

            return null;
        }

        _container.Inject(enemy);

        enemy.SetConfig(currentConfig);
        enemy.Initialize();
        enemy.SetPool(pool);

        return enemy;
    }
}
