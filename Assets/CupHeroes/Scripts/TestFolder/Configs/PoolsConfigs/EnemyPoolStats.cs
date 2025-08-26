using System;
using UnityEngine;

[Serializable]
public class EnemyPoolStats : PoolStats
{
    [field: SerializeField] public EnemyType Type { get; private set; }
}
