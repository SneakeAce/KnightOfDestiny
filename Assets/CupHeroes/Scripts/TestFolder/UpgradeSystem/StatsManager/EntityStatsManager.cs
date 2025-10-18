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

    public void ModifyStats(float value, UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.IncreaseAttackDamageUpgrade:
                AttackStats.ModifyDamage(value); 
                break;
            case UpgradeType.IncreaseAttackSpeedUpgrade:
                AttackStats.ModifyAttackSpeed(value); 
                break;
            case UpgradeType.IncreaseMaxHealthUpgrade:
                HealthStats.ModifyHealth(value);
                break;
            //case UpgradeType.IncreaseRegenerationValueHealthUpgrade:
            //    HealthStats.ModifyHealthRegenerationValue(value);
            //    break;
            //case UpgradeType.IncreaseRegenerationSpeedHealthUpgrade:
            //    HealthStats.ModifyHealthRegenerationSpeed(value);
            //    break;
        }
    }
}
