using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackTypeSwitcher
{
    private ICharacter _character;

    private EntityAttackType _currentAttackType;
    private List<EntityAttackType> _availableTypes = new List<EntityAttackType>();

    public AttackTypeSwitcher(ICharacter character)
    {
        _character = character;
    }

    public event Action OnAttackTypeChanged;

    public EntityAttackType CurrentAttackType { get => GetAttackType(); }

    public void Initialize()
    {
        _availableTypes = GetAvailableTypes();

        _currentAttackType = GetBaseType();
    }

    public void CheckDistanceToTargetAndSwitch(float distance, float range)
    {
        EntityAttackType newAttackType;

        newAttackType = distance <= range ?
            EntityAttackType.Melee :
            EntityAttackType.Range;

        if (newAttackType != _currentAttackType)
        {
            _currentAttackType = newAttackType;
            OnAttackTypeChanged?.Invoke();
        }
    }

    private EntityAttackType GetAttackType()
    {
        return _currentAttackType;
    }

    private EntityAttackType GetBaseType()
    {
        foreach (var type in _availableTypes)
        {
            if (type == _character.Config.AttackStats.BaseAttackType)
                return type;
        }

        return EntityAttackType.None;
    }

    private List<EntityAttackType> GetAvailableTypes()
    {
        var list = Enum.GetValues(typeof(EntityAttackType))
            .Cast<EntityAttackType>()
            .Where(type => type != EntityAttackType.None &&
            (_character.Config.AttackStats.AvailableAttackTypes & type) != 0)
            .ToList();

        if (list.Count <= 0)
            return new List<EntityAttackType>();

        return list;
    }
}
