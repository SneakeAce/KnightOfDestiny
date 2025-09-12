using System;
using UnityEngine;
using Zenject;

public class CharacterController : ICharacterController, ITickable, IDisposable
{
    private const float MinDistanceBetweenCharacterAndPoint = 0.2f;

    private IEntity _character;
    private IEnemy _currentTarget;

    private ICommandInvoker _commandInvoker;
    private ICommand _currentCommand;

    private TargetFinder _targetFinder;
    private CoroutinePerformer _coroutinePerformer;

    private Vector2 _positionToMove;

    private float _distanceBetweenCharacterAndPoint;

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
        _character = entity;

        InitializeTargetFinder();
    }

    public void Dispose()
    {
        _targetFinder.OnTargetFound -= SetTarget;
    }

    public void Tick()
    {
        if (_isMoving && Vector2.Distance(_character.Transform.position, _positionToMove) <= MinDistanceBetweenCharacterAndPoint)
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
        _targetFinder = new TargetFinder(_character, _coroutinePerformer);

        _targetFinder.OnTargetFound += SetTarget;

        _targetFinder.Initialize();
    }

    private void SetTarget(IEnemy enemy)
    {
        _currentTarget = enemy;

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

        _currentCommand = new AttackCommand(_character, _currentTarget, _coroutinePerformer);

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
