public interface IEntityStatsManager
{ 
    IHealthStatsController HealthStats { get; }
    IAttackStatsController AttackStats { get; }

    void Initialize(IEntity entity);
    void ModifyStats(float value, UpgradeType type);
}
