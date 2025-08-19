using System;
using UnityEngine;

public class EnemyPoolsFactory : IEnemyPoolsFactory
{
    public ObjectPool<Enemy> CreatePool(GameObject prefab, PoolStats poolStats)
    {
        GameObject container = GameObject.Instantiate(poolStats.Container.gameObject);

        PoolCreatingArguments args = new PoolCreatingArguments(
            prefab,
            poolStats.MaxCountEntitiesInPool, 
            poolStats.PoolCanExpand, 
            container.transform);

        ObjectPool<Enemy> pool = new ObjectPool<Enemy>(args);

        pool.CreatePool();

        if (pool == null)
            throw new ArgumentNullException("pool in PoolsFactory is null!");

        return pool;
    }
}
