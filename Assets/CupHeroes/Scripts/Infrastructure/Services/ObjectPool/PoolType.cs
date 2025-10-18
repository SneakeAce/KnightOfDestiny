using System;

[Flags]
public enum PoolType
{
    None = 0,
    EnemyEntityPool = 1 << 0,
    UIObjectPool = 1 << 1,
    ProjectilePool = 1 << 2,
    UpgradeCardPool = 1 << 3,
}
