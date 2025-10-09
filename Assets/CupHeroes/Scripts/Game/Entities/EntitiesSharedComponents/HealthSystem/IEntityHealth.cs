using System;

public interface IEntityHealth : IDamageable
{
    float CurrentHealth { get; }
    float MaxHealth { get; }

    event Action<float, float> CurrentHealthChanged;
    event Action<float> OnTakingDamage;
    event Action<IEntity> EntityDied;

    void Initialize(IEntity entity);
    void InitialHealth(float baseValue);
    void ModifyHealth(float value);
    void ResetHealthValueToBase();
}
