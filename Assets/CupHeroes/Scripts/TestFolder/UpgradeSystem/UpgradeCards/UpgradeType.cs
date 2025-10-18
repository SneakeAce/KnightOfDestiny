using System;

[Flags]
public enum UpgradeType
{
    None = 0,

    IncreaseAttackDamageUpgrade = 1 << 0,
    IncreaseAttackSpeedUpgrade = 1 << 1,
    IncreaseMaxHealthUpgrade = 1 << 2,
    //IncreaseRegenerationValueHealthUpgrade = 1 << 3,
    //IncreaseRegenerationSpeedHealthUpgrade = 1 << 4,

    //DecreaseAttackDamageUpgrade = 1 << 5,
    //DecreaseAttackSpeedUpgrade = 1 << 6,
    //DecreaseMaxHealthUpgrade = 1 << 7,
    //DecreaseRegenerationValueHealthUpgrade = 1 << 8,
    //DecreaseRegenerationSpeedHealthUpgrade = 1 << 9,
}
