using System;
using UnityEngine;

public abstract class EntityHealth : IEntityHealth
{
    protected const float MinHealthValue = 0f;
    
    protected float _currentHealth;
    protected float _maxHealth;

    private float _baseHealthValue;

    protected IEntity _entity;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    public event Action<float, float> CurrentHealthChanged;
    public event Action<float> OnTakingDamage;
    public event Action<IEntity> EntityDied;

    public virtual void Initialize(IEntity entity)
    {
        _entity = entity;
    }

    public void TakeDamage(DamageData damageData)
    {
        if (damageData.Damage > 0)
            _currentHealth = _currentHealth - damageData.Damage;
        else
            Debug.Log($"EnemyHealth. Damage is 0 or negative damage = {damageData.Damage}");

        CurrentHealthChanged?.Invoke(_currentHealth, _maxHealth);
        OnTakingDamage?.Invoke(damageData.Damage);

        //Debug.Log($"EnemyHealth. TakeDamage in Health. damage = {damageData.Damage}");

        if (_currentHealth <= MinHealthValue)
        {
            EntityDied?.Invoke(_entity);
            //Debug.Log($"EnemyHealth. {_entity} died: ");
        }
    }

    public void InitialHealth(float baseValue)
    {
        _baseHealthValue = baseValue;

        _maxHealth = _baseHealthValue;
        _currentHealth = _maxHealth;

        CurrentHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void ModifyHealth(float value)
    {
        _currentHealth += value;
        _maxHealth += value;

        CurrentHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void ResetHealthValueToBase()
    {
        _maxHealth = _baseHealthValue;
        _currentHealth = _maxHealth;
    }
}
