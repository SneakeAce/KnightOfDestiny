using System;
using UnityEngine;

[Serializable]
public class ProjectileStats
{
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public bool IsSplashAttack { get; private set; }
    [field: SerializeField] public float SplashRadius { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
}
