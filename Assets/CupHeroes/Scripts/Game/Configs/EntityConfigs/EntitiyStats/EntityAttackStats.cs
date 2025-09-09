using System;
using UnityEngine;

[Serializable]
public class EntityAttackStats
{
    private const float MinAttackSpeed = 1f;
    private const float DividerForCoversionToProcetage = 100f;

    [field: SerializeField] public float BaseDamage { get; private set; }
    [field: SerializeField, Range(0f, 200f)] public float BaseAttackSpeedProcent { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public EntityAttackType AttackType { get; private set; }
    [field: SerializeField] public LayerMask TargetLayer { get; private set; }
    [field: SerializeField] public AnimationClip AttackClip { get; private set; }


    // Creation a separate class for working these stats!
    public float CurrentAttackSpeedProcent { get; private set; }
    public float BaseAttacksPerSecond => BaseAttackSpeedProcent / DividerForCoversionToProcetage;
    public float CurrentAttacksPerSecond => CurrentAttackSpeedProcent / DividerForCoversionToProcetage;

    public void ResetAttackSpeedToBase() => CurrentAttackSpeedProcent = BaseAttackSpeedProcent;
    public void ApplyProcentModifier(float value) => 
        CurrentAttackSpeedProcent = Mathf.Max(MinAttackSpeed, CurrentAttackSpeedProcent + value);
}
