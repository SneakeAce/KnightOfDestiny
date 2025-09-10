using System;
using UnityEngine;

[Serializable]
public class EntityMainStats
{
    [field: SerializeField] public EntityMoveStats MoveStats { get; private set; }
    [field: SerializeField] public EntityHealthStats HealthStats { get; private set; }
    [field: SerializeField] public EntityAttackStats AttackStats { get; private set; }

}
