using UnityEngine;
using Zenject;

public class EnemyFactory : IEnemyFactory
{
    private DiContainer _container;
    private IEnemyPoolManager _poolManager;

    public EnemyFactory(DiContainer container, IEnemyPoolManager poolManager)
    {
        _container = container;
        _poolManager = poolManager;
    }

    public Enemy CreateEnemy()
    {
        ObjectPool<Enemy> pool = _poolManager.GetRandomPool();

        Debug.Log($"EnemyFactory pool = {pool}");

        Enemy enemy = (Enemy)pool.GetObjectFromPool();

        Debug.Log($"EnemyFactory enemy = {enemy}");

        if (enemy == null)
            return null;

        EnemyConfig config = _poolManager.GetConfig(pool); 

        _container.Inject(enemy);

        enemy.SetConfig(config);
        enemy.Initialize();

        return enemy;
    }
}
