using System;

public interface IEntityHealth : IDamageable
{
    void Initialize(IEntity entity, float initialHealth);

    float CurrentHealth { get; }
    float MaxHealth { get; }

    event Action<float, float> CurrentHealthChanged;
    event Action<float> OnTakingDamage;
    event Action<IEntity> EntityDied;
}
