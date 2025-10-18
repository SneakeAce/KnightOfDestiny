using System;
using UnityEngine;

[Serializable]
public class UpgradeCardPoolStats : PoolStats
{
    [field: SerializeField] public UpgradeType Type { get; private set; }
}
