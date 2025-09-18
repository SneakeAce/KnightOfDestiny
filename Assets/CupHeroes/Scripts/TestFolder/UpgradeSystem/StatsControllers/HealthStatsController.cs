public class HealthStatsController : IHealthStatsController
{
    private IEntity _entity;
    private IEntityHealth _entityHealth;

    private float _baseHealthValue;

    public void Initialize(IEntity entity)
    {
        _entity = entity;
        SetBaseParameters();
    }

    public void ModifyHealth(float value)
    {
        _entityHealth.ModifyHealth(value);
    }

    public void ResetValues()
    {
        _entityHealth.ResetHealthValueToBase();
    }

    private void SetBaseParameters()
    {
        _entityHealth = _entity.Health;
        _baseHealthValue = _entity.Config.HealthStats.BaseValueHealth;

        _entityHealth.InitialHealth(_baseHealthValue);
    }
}
