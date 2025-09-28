using System;

public interface IProjectileAttackStrategy : IAttackStrategy
{
    event Action ProjectileCollided;
    void Initialize(ProjectileAttackData data);
}
