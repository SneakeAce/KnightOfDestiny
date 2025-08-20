using System;

public class HealthBarController : IDisposable
{
    private IEntity _entity;
    private HealthBarView _healthBarView;
   // private DamagePopupController _damagePopupController;

    public HealthBarController(IEntity entity, HealthBarView healthBarView)
    {
        _entity = entity;
        _healthBarView = healthBarView;
       // _damagePopupController = damagePopupController;
    }

    public void Dispose()
    {
        _entity.Health.CurrentHealthChanged -= _healthBarView.SetHealth;
       // _entity.Health.OnTakingDamage -= _damagePopupController.ShowPopup;

    }

    public void Initialize()
    {
        _entity.Health.CurrentHealthChanged += _healthBarView.SetHealth;
       // _entity.Health.OnTakingDamage += _damagePopupController.ShowPopup;

        _healthBarView.SetHealth(_entity.Health.CurrentHealth, _entity.Health.MaxHealth);
    }
}
