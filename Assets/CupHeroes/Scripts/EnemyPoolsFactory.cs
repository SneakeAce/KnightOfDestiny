using System;

public class EnemyPoolsFactory : IEnemyPoolsFactory
{
    public ObjectPool<Enemy> CreatePool(PoolStats poolStats)
    {
        PoolCreatingArguments args = new PoolCreatingArguments(
            poolStats.MaxCountEntitiesInPool, 
            poolStats.PoolCanExpand, 
            null);

        ObjectPool<Enemy> pool = new ObjectPool<Enemy>(args);

        if (pool == null)
            throw new ArgumentNullException("pool in PoolsFactory is null!");

        return pool;
    }
}
