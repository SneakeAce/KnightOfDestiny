using System;
using UnityEngine;

[Serializable]
public class UpgradeTypePallete : Pallete
{
    [field: SerializeField] public UpgradeType UpgradeType { get; private set; }
}
