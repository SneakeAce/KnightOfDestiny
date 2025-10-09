using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CharacterAttackController : ITickable, IDisposable
{
    private ICharacter _character;
    private ICharacterController _characterController;

    private IEntity _soloTarget;

    private IAttackCommand _currentCommand;

    private IEntityAttackStrategy _currentAttackStrategy;

    private CoroutinePerformer _performer;
    private ProjectileSpawner _projectileSpawner;

    private TargetFinderController _targetFinderController;
    private AttackTypeSwitcher _attackTypeSwitcher;

    private EntityAttackType _currentAttackType;
    private List<IEntity> _targets;

    public CharacterAttackController(CoroutinePerformer performer, ProjectileSpawner projectileSpawner)
    {
        _performer = performer;
        _projectileSpawner = projectileSpawner;
    }

    public event Action<IEntity> TargetSwitched;
    public event Action<List<IEntity>> SendTargets;

    public void Dispose()
    {
        _characterController.IsCharacterOnPosition -= ExecuteAttackCommand;

        _attackTypeSwitcher.OnAttackTypeChanged -= SwitchAttackStrategy;

        _targetFinderController.TargetsFounded -= OnGetTargets;
        _targetFinderController.ClosestTargetFounded -= OnTargetSwitched;

        if (_currentAttackStrategy != null)
            TargetSwitched -= _currentAttackStrategy.SwitchTarget;
    }

    public void Tick()
    {
        if (_soloTarget == null || _character == null)
            return;

        var distance = Vector2.Distance(_soloTarget.Transform.position, _character.Transform.position);
        var rangeAttack = _character.StatsManager.AttackStats.MeleeAttackRange;

        _attackTypeSwitcher.CheckDistanceToTargetAndSwitch(distance, rangeAttack);
    }

    public void Initialize(ICharacterController controller)
    {
        _characterController = controller;
        _character = _characterController.Character;

        InitializeAttackTypeSwitcher();

        InitializeTargetFinder();

        SubscribingEvents();
    }

    private void InitializeAttackTypeSwitcher()
    {
        _attackTypeSwitcher = new AttackTypeSwitcher(_character);

        _attackTypeSwitcher.Initialize();

        _currentAttackType = _character.StatsManager.AttackStats.CurrentAttackType;
    }

    private void InitializeTargetFinder()
    {
        _targetFinderController = new TargetFinderController(_character, _performer);

        _targetFinderController.Initialize();
    }

    private void SubscribingEvents()
    {
        _characterController.IsCharacterOnPosition += ExecuteAttackCommand;

        _attackTypeSwitcher.OnAttackTypeChanged += SwitchAttackStrategy;

        _targetFinderController.TargetsFounded += OnGetTargets; 
        _targetFinderController.ClosestTargetFounded += OnTargetSwitched;
    }

    private void SwitchAttackStrategy()
    {
        TargetSwitched -= _currentAttackStrategy.SwitchTarget;

        _character.StatsManager.AttackStats.SwitchAttackType(_attackTypeSwitcher.CurrentAttackType);

        _currentAttackType = _character.StatsManager.AttackStats.CurrentAttackType;

        _currentCommand.UpdateState(GetAttackStrategy());

        SendTargets?.Invoke(_targets);
        TargetSwitched?.Invoke(_soloTarget);
    }

    private IEntityAttackStrategy GetAttackStrategy()
    {
        IEntityAttackStrategy strategy = null;

        if (_currentAttackType == EntityAttackType.Melee)
        {
            if (_character.Config.AttackStats.CanAttackMultipleTargets)
            {
                strategy = new MeleeAttackByMultipleTargets();

                Debug.Log($"{this.ToString()} - GetAttackStrategy - _strategy = {strategy}");
            }
            else
            {
                strategy = new MeleeAttackByOnceTarget();
            }
        }
        else if (_currentAttackType == EntityAttackType.Range)
        {
            strategy = new RangeAttackByOnceTarget(_projectileSpawner);
        }

        _currentAttackStrategy = strategy;

        TargetSwitched += _currentAttackStrategy.SwitchTarget;
        SendTargets += _currentAttackStrategy.GetTargets;

        return strategy;
    }

    private void OnGetTargets(IEnumerable<IEntity> enemies)
    {
        if (enemies.Count() == 0)
            return;

        _targets = enemies.ToList();
        _soloTarget = enemies.FirstOrDefault();

        SendTargets?.Invoke(_targets);

        if (_currentCommand == null)
            ExecuteAttackCommand();
    }

    private void OnTargetSwitched(IEntity newTarget)
    {
        _soloTarget = newTarget;

        TargetSwitched?.Invoke(newTarget);

        if (_currentCommand != null)
            _currentCommand.RestartStrategyAtState();
    }

    private void ExecuteAttackCommand()
    {
        IEntityAttackStrategy strategy = GetAttackStrategy();

        _character.EntityController.SetAttackCommand(strategy);

        _currentCommand = (IAttackCommand)_characterController.GetCurrentCommand();
    }
}

