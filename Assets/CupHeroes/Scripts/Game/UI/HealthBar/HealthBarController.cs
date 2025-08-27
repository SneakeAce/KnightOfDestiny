using System;
using UnityEngine;

public class HealthBarController : IDisposable
{
    private IEntity _entity;
    private HealthBarView _healthBarView;

    private Transform _parent;

    public HealthBarController(IEntity entity, HealthBarView healthBarView)
    {
        _entity = entity;
        _healthBarView = healthBarView;
    }

    public void Dispose()
    {
        _entity.Health.CurrentHealthChanged -= _healthBarView.SetHealth;
        _entity.Health.EntityDied -= ReturnHealthBarToPool;
    }

    public void Initialize()
    {
        _entity.Health.CurrentHealthChanged += _healthBarView.SetHealth;
        _entity.Health.EntityDied += ReturnHealthBarToPool;

        _healthBarView.SetHealth(_entity.Health.CurrentHealth, _entity.Health.MaxHealth);

        _parent = _healthBarView.transform.parent;
    }

    private void ReturnHealthBarToPool(IEntity entity)
    {
        _healthBarView.transform.SetParent(_parent);

        _healthBarView.ReturnToPool();
    }
}
