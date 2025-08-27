using System;
using UnityEngine;

public class EntityHealth : IEntityHealth
{
    private const float MinHealthValue = 0f;

    private float _currentHealth;
    private float _maxHealth;

    private IEntity _entity;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    public event Action<float, float> CurrentHealthChanged;
    public event Action<float> OnTakingDamage;
    public event Action<IEntity> EntityDied;

    public void Initialize(IEntity entity, float initialHealth)
    {
        _entity = entity;

        _maxHealth = initialHealth;
        _currentHealth = _maxHealth;

        CurrentHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void TakeDamage(DamageData damageData)
    {
        if (damageData.Damage > 0)
            _currentHealth = _currentHealth - damageData.Damage;
        else
            Debug.Log($"Damage is 0 or negative damage = {damageData.Damage}");

        CurrentHealthChanged?.Invoke(_currentHealth, _maxHealth);
        OnTakingDamage?.Invoke(damageData.Damage);

        Debug.Log($"TakeDamage in Health. damage = {damageData.Damage}");

        if (_currentHealth <= MinHealthValue)
        {
            EntityDied?.Invoke(_entity);
            Debug.Log("Entity died: " + _entity);
        }
    }
}
