using System.Collections.Generic;
using UnityEngine;

public class AttackTypeSwitcher
{
    private IEntity _character;

    private EntityAttackType _currentAttackType;
    private List<EntityAttackType> _availableAttackType;

    public EntityAttackType CurrentAttackType { get => _currentAttackType; }

    public void Initialize()
    {

    }

    public EntityAttackType SwitchAttackType()
    {
        return EntityAttackType.None;
    }

    public EntityAttackType GetAttackType()
    {
        return _currentAttackType;
    }

    private List<EntityAttackType> GetAvailableAttackTypes()
    {
        return new List<EntityAttackType>();
    }

}
