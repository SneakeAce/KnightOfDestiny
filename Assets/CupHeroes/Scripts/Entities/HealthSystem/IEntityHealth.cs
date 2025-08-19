using System;

public interface IEntityHealth : IDamageable
{
    event Action<float> CurrentHealthChanged;
    event Action<float> MaxHealthChanged;
    event Action<IEntity> EntityDied;
}
