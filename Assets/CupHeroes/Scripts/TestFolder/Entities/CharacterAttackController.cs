using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CharacterAttackController : ITickable, IDisposable
{
    // Передаем таргеты в зависимости от их количества.
    // Проверяем тип атаки, ближний бой или дальний.
    // Если цель одна, то вызываем атаку по одиночной цели, если много,то по множественным целям. 

    // Если атака дальнего боя, то вызываем атаку дальнего боя, которая спавнит снаряд,
    // который будет лететь в цель, но если у снаряда стоит флаг сплэш урона,
    // то снаряд нанесет урон всем врагам в радиусе

    private ICharacter _character;
    private ICharacterController _characterController;

    private IEntity _enemy;

    private IAttackCommand _currentCommand;

    private ITargetFinderStrategy _targetFinderStrategy;

    private CoroutinePerformer _performer;
    private TargetFinderContext _targetFinderContext;
    private AttackTypeSwitcher _attackTypeSwitcher;

    private EntityAttackType _currentAttackType;
    private IEnumerable<IEntity> _targets = new List<IEntity>();

    private bool _isAttackCommandInitialized;

    public CharacterAttackController(CoroutinePerformer performer)
    {
        _performer = performer;
    }

    public void Dispose()
    {
        _characterController.IsCharacterOnPosition -= ExecuteAttackCommand;

        _attackTypeSwitcher.OnAttackTypeChanged -= SwitchAttackStrategy;

        _targetFinderContext.OnTargetsFound -= GetTargets;
    }

    public void Tick()
    {
        if (_enemy == null)
            return;

        var distance = Vector2.Distance(_enemy.Transform.position, _character.Transform.position);
        var meleeRange = _character.StatsManager.AttackStats.MeleeAttackRange;

        _attackTypeSwitcher.CheckDistanceToTargetAndSwitch(distance, meleeRange);
    }

    public void Initialize(ICharacter character)
    {
        _character = character;

        _characterController = (ICharacterController)_character.EntityController;

        _isAttackCommandInitialized = false;

        _attackTypeSwitcher = new AttackTypeSwitcher(_character);

        _attackTypeSwitcher.Initialize();
        _currentAttackType = _attackTypeSwitcher.CurrentAttackType;

        InitializeTargetFinder();

        SubscribingEvents();
    }

    private void SubscribingEvents()
    {
        _characterController.IsCharacterOnPosition += ExecuteAttackCommand;

        _attackTypeSwitcher.OnAttackTypeChanged += SwitchAttackStrategy;

        _targetFinderContext.OnTargetsFound += GetTargets;
    }

    private void SwitchAttackStrategy()
    {
        _currentCommand.SwitchState(GetAttackStrategy());
    }

    private void InitializeTargetFinder()
    {
        _targetFinderStrategy = GetTargetFinderStrategy();

        _targetFinderContext = new TargetFinderContext((ICharacter)_character, _targetFinderStrategy, _performer);

        _targetFinderContext.Initialize();
    }

    private ITargetFinderStrategy GetTargetFinderStrategy()
    {
        ITargetFinderStrategy strategy = null;

        if (_character.Config.AttackStats.CanFindMultipleTargets)
        {
            strategy = new FindMultipleTargets();
        }
        else
        {
            strategy = new FindOnceTarget();
        }

        return strategy;
    }

    private IEntityAttackStrategy GetAttackStrategy()
    {
        IEntityAttackStrategy strategy = null;

        if (_currentAttackType == EntityAttackType.Melee)
        {
            if (_character.Config.AttackStats.CanFindMultipleTargets)
            {
                var listTargets = _targets.ToList();

                strategy = new MeleeAttackByMultipleTargets(listTargets);
            }
            else
            {
                var soloTarget = _targets.FirstOrDefault();

                strategy = new MeleeAttackByOnceTarget(soloTarget);
            }
        }
        else if (_currentAttackType == EntityAttackType.Range)
        {
            if (_character.Config.AttackStats.CanFindMultipleTargets)
            {
                var listTargets = _targets.ToList();

                strategy = new MeleeAttackByMultipleTargets(listTargets);
            }
            else
            {
                var soloTarget = _targets.FirstOrDefault();

                strategy = new MeleeAttackByOnceTarget(soloTarget);
            }
        }

        return strategy;
    }

    private void GetTargets(IEnumerable<IEntity> enemies)
    {
        if (enemies.Count() == 0)
            return;

        _targets = enemies;
        _enemy = enemies.FirstOrDefault();

        if (_isAttackCommandInitialized)
        {
            return;
        }
        else
        {
            ExecuteAttackCommand();
        }
    }

    private void ExecuteAttackCommand()
    {
        IEntityAttackStrategy strategy = GetAttackStrategy();

        _character.EntityController.SetAttackCommand(strategy);

        _currentCommand = (IAttackCommand)_characterController.GetCurrentCommand();
    }
}

