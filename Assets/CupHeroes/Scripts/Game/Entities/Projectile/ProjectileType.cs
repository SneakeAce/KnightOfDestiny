using System;

[Flags]
public enum ProjectileType
{
    None = 0,
    BaseProjectile = 1 << 0,
    ExplosionProjectile = 1 << 1,
    BouncingProjectile = 1 << 2,
}
