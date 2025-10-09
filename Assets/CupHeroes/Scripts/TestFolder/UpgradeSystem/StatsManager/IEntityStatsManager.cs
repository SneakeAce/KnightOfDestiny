public interface IEntityStatsManager
{ 
    IHealthStatsController HealthStats { get; }
    IAttackStatsController AttackStats { get; }
    void Initialize(IEntity entity);

}
