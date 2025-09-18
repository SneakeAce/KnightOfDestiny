public class EntityStatsManager : IEntityStatsManager
{
    private IHealthStatsController _healthStatsController;
    private IAttackStatsController _attackStatsController;

    public EntityStatsManager(IHealthStatsController healthStatsController, 
        IAttackStatsController attackStatsController)
    {
        _healthStatsController = healthStatsController;
        _attackStatsController = attackStatsController;
    }

    public IHealthStatsController HealthStats => _healthStatsController;
    public IAttackStatsController AttackStats => _attackStatsController;

    public void Initialize(IEntity entity)
    {
        _healthStatsController.Initialize(entity);
        _attackStatsController.Initialize(entity);
    }
}
