using System;
using UnityEngine;

/// <summary>
/// When adding a new field, add it to the <see cref="ProjectileStatsDrawer"/>
/// </summary>
[Serializable]
public class ProjectileStats
{
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public bool IsSplashAttack { get; private set; }
    [field: SerializeField] public float SplashRadius { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float DistanceFlying { get; private set; } 
}
