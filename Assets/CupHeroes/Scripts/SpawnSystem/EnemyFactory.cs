using UnityEngine;
using Zenject;

public class EnemyFactory : IEntityFactory
{
    private DiContainer _container;
    private IEnemyPoolManager _poolManager;

    public EnemyFactory(DiContainer container, IEnemyPoolManager poolManager)
    {
        _container = container;
        _poolManager = poolManager;
    }

    public GameObject CreateEntity()
    {
        ObjectPool<Enemy> pool = _poolManager.GetRandomPool();

        Enemy enemy = (Enemy)pool.GetObjectFromPool();
        
        if (enemy == null)
            return null;

        EnemyConfig config = _poolManager.GetConfig(pool); 

        _container.Inject(enemy);

        enemy.SetConfig(config);
        enemy.Initialize();

        return enemy.gameObject;
    }
}
