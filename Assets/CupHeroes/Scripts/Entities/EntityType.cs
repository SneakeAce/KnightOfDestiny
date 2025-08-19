using System;

[Flags]
public enum EntityType
{
    None = 0,
    PlayerEntity = 1 << 0,
    EnemyEntity = 1 << 1,
}
