using System;

[Flags]
public enum EnemyType
{
    None = 0, 
    BaseEnemy = 1 << 0,
    StrengthEnemy = 1 << 1,

}
