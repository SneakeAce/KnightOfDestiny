using System;
using UnityEngine;

[Serializable]
public class EntityAttackStats
{
    [field: SerializeField] public int BaseAmountTargetsForAttack { get; private set; }
    [field: SerializeField] public bool CanFindMultipleTargets { get; private set; }
    [field: SerializeField] public float BaseDamage { get; private set; }
    [field: SerializeField, Range(0.1f, 200f)] public float BaseAttackSpeedProcent { get; private set; }
    [field: SerializeField] public float BaseAttackRange { get; private set; }
    [field: SerializeField] public EntityAttackType AttackType { get; private set; }
    [field: SerializeField] public LayerMask TargetLayer { get; private set; }
    [field: SerializeField] public AnimationClip AttackClip { get; private set; }
}
