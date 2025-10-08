using System;
using UnityEngine;

[Serializable]
public class EntityAttackStats
{
    [field: SerializeField] public int BaseAmountTargetsForAttack { get; private set; }
    [field: SerializeField] public bool CanFindMultipleTargets { get; private set; }

    [field: SerializeField] public float BaseDamage { get; private set; }
    [field: SerializeField, Range(0.1f, 1000f)] public float BaseAttackSpeedProcent { get; private set; }
    [field: SerializeField] public float BaseMeleeAttackRange { get; private set; }
    [field: SerializeField] public float BaseRangeAttackRange { get; private set; }

    [field: SerializeField] public EntityAttackType BaseAttackType { get; private set; }
    [field: SerializeField] public EntityAttackType AvailableAttackTypes { get; private set; }
    [field: SerializeField] public LayerMask TargetLayer { get; private set; }
    [field: SerializeField] public AnimationClip AttackClip { get; private set; }

    [field: SerializeField] public ProjectileType AvailableProjectileType { get; private set; }
}
