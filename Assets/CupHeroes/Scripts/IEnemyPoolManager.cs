public interface IEnemyPoolManager
{
    void Initialize();
    ObjectPool<Enemy> GetPool(EntityAttackType attackType);
    ObjectPool<Enemy> GetRandomPool();
    EnemyConfig GetConfig(ObjectPool<Enemy> pool);
}
