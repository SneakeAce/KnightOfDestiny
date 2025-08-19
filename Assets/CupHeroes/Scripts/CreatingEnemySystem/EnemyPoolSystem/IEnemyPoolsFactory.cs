using UnityEngine;

public interface IEnemyPoolsFactory
{
    ObjectPool<Enemy> CreatePool(GameObject prefab, PoolStats poolStats);
}
