using System;

[Flags]
public enum EnemyType
{
    None = 0, 
    BaseMeleeEnemy = 1 << 0,
    BaseRangeEnemy = 1 << 1,
    StrenghtMeleeEnemy = 1 << 2,
    StrenghtRangeEnemy = 1 << 3,

}
