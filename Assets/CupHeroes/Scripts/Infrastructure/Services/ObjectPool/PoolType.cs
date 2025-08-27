using System;

[Flags]
public enum PoolType
{
    None = 0,
    EnemyEntityPool = 1 << 0,
    UIObjectPool = 1 << 1,

}
