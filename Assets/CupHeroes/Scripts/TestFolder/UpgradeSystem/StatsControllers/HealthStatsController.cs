using Unity.VisualScripting;

public class HealthStatsController : IHealthStatsController
{
    private IEntity _entity;
    private IEntityHealth _entityHealth;

    private float _baseHealthValue;
    private float _baseRegenerationValue;
    private float _baseRegenerationSpeed;

    public void Initialize(IEntity entity)
    {
        _entity = entity;
        SetBaseParameters();
    }

    public void ModifyHealth(float value)
    {
        _entityHealth.ModifyHealth(value);

        UnityEngine.Debug.Log($"{this.ToString()} - ModifyDamage - maxHealth = {_entity.Health.MaxHealth}");
    }

    public void ModifyHealthRegenerationValue(float value)
    {
        _baseRegenerationValue += value;
    }

    public void ModifyHealthRegenerationSpeed(float value)
    {
        _baseRegenerationSpeed += value;
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
