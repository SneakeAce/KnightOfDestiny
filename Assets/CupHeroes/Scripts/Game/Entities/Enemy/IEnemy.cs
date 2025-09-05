public interface IEnemy : IEntity
{
    EnemyController Controller { get; }

    void SetController(EnemyController controller);
    void SetPool(IObjectPool currentPool);
}
