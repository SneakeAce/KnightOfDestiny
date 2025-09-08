using System;
using UnityEngine;

[Serializable]
public class EntityAttackStats
{
    [field: SerializeField] public float BaseDamage { get; private set; }
    [field: SerializeField] public float BaseAttackSpeed { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public EntityAttackType AttackType { get; private set; }
    [field: SerializeField] public LayerMask TargetLayer { get; private set; }
    [field: SerializeField] public AnimationClip AttackClip { get; private set; }
}
