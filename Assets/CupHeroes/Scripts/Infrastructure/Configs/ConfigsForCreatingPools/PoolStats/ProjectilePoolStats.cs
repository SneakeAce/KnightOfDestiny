using System;
using UnityEngine;

[Serializable]
public class ProjectilePoolStats : PoolStats
{
    [field: SerializeField] public ProjectileType ProjectileType { get; private set; }

}
