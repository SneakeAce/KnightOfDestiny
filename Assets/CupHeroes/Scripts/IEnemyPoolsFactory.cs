public interface IEnemyPoolsFactory
{
    ObjectPool<Enemy> CreatePool(PoolStats poolStats);
}
