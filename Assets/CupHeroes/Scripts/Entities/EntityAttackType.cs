using System;

[Flags]
public enum EntityAttackType
{
    None = 0,
    Melee = 1 << 0,
    Range = 1 << 1,
}
