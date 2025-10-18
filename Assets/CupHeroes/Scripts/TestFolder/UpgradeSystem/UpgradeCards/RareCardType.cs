using System;

[Flags]
public enum RareCardType
{
    None = 0,
    Common = 1 << 0,
    Rare = 1 << 1,
    Epic = 1 << 2,
    Legendary = 1 << 3,
}
