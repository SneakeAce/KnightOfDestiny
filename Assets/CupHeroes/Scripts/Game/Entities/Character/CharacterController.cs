using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CharacterController : ICharacterController, ITickable, IDisposable
{
    private const float MinDistanceBetweenCharacterAndPoint = 0.2f;

    private ICharacter _character;

    private ICommandInvoker _commandInvoker;
    private ICommand _currentCommand;

    private IAttackStrategy _currentAttackStrategy;

    private TargetFinderContext _targetFinder;
    private CoroutinePerformer _coroutinePerformer;

    private Vector2 _positionToMove;

    private bool _isMoving;

    public CharacterController(ICommandInvoker commandInvoker, 
        CoroutinePerformer coroutinePerformer)
    {
        _commandInvoker = commandInvoker;
        _coroutinePerformer = coroutinePerformer;
    }

    public event Action IsCharacterOnPosition;

    public void Initialize(IEntity entity)
    {
        _character = (ICharacter)entity;

        InitializeTargetFinder();
    }

    public void Dispose()
    {
        _targetFinder.OnTargetsFound -= SetTarget;
    }

    public void Tick()
    {
        if (_isMoving && Vector2.Distance(_character.Transform.position, _positionToMove) <= 
            MinDistanceBetweenCharacterAndPoint)
        {
            _isMoving = false;
            IsCharacterOnPosition?.Invoke();
        }
    }

    public void SetMoveCommand()
    {
        AddMoveCommand();
    }

    public void SetAttackCommand()
    {
        AddAttackCommand();
    }

    public void SetIdleCommand()
    {
        AddIdleCommand();
    }

    public void SetPositionToMove(Vector2 position)
    {
        _positionToMove = (Vector2)_character.Transform.position + position;
    }

    private void InitializeTargetFinder()
    {
        if (_character.Config.AttackStats.CanFindMultipleTargets == false)
        {
            var strategy = new FindOnceTarget();

            _targetFinder = new TargetFinderContext(_character, strategy, _coroutinePerformer);

            _targetFinder.OnTargetsFound += SetTarget;
        }
        else
        {
            var strategy = new FindMultipleTargets();

            _targetFinder = new TargetFinderContext(_character, strategy, _coroutinePerformer);

            _targetFinder.OnTargetsFound += SetTargets;
        }

        _targetFinder.Initialize();
    }

    private void SetTarget(IEnumerable<IEnemy> enemies)
    {
        Debug.Log("SetTarget");

        var currentTarget = enemies.FirstOrDefault();

        _currentAttackStrategy?.Dispose();

        _currentAttackStrategy = null;

        _currentAttackStrategy = new AttackByOnceTarget(currentTarget);

        SetAttackCommand();
    }

    private void SetTargets(IEnumerable<IEnemy> enemies)
    {
        Debug.Log("SetTargets");
        var targets = enemies
            .Cast<IEntity>()
            .ToList();

        _currentAttackStrategy?.Dispose();

        _currentAttackStrategy = null;

        _currentAttackStrategy = new AttackByMultipleTargets(targets);

        SetAttackCommand();
    }

    private void AddMoveCommand()
    {
        _currentCommand?.CancelCommand();

        _currentCommand = null;

        _currentCommand = new MoveCommand(_character, _positionToMove);

        _isMoving = true;

        ExecuteCommand();
    }

    private void AddAttackCommand()
    {
        _currentCommand?.CancelCommand();

        _currentCommand = null;

        _currentCommand = new AttackCommand(_character, _currentAttackStrategy, _coroutinePerformer);

        ExecuteCommand();
    }

    private void AddIdleCommand()
    {
        _currentCommand?.CancelCommand();

        _currentCommand = null;

        _currentCommand = new IdleCommand(_character);

        ExecuteCommand();
    }

    private void ExecuteCommand()
    {
        _commandInvoker.AddCommand(_currentCommand);
        _commandInvoker.ExecuteCommand();
    }

}
