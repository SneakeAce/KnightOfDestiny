using System;
using UnityEngine;

[Serializable]
public class EntityMainStats
{
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public float BaseValueHealth { get; private set; }
    [field: SerializeField] public float BaseDamage { get; private set; }
    [field: SerializeField] public float BaseAttackSpeed { get; private set; }
    [field: SerializeField] public EntityAttackType AttackType { get; private set; }

}
