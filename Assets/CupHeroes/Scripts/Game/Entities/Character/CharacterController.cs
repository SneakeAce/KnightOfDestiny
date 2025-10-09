using System;
using UnityEngine;
using Zenject;

public class CharacterController : ICharacterController, ITickable, IDisposable
{
    private const float MinDistanceBetweenCharacterAndPoint = 0.2f;

    private ICharacter _character;

    private ICommandInvoker _commandInvoker;
    private ICommand _currentCommand;

    private CharacterAttackController _attackController;
    private CoroutinePerformer _coroutinePerformer;

    private Vector2 _positionToMove;

    private bool _isMoving;


    public CharacterController(ICommandInvoker commandInvoker, CharacterAttackController attackController, 
        CoroutinePerformer coroutinePerformer)
    {
        _commandInvoker = commandInvoker;
        _attackController = attackController;
        _coroutinePerformer = coroutinePerformer;
    }

    public ICharacter Character => _character;

    public event Action IsCharacterOnPosition;

    public void Initialize(IEntity entity)
    {
        _character = (ICharacter)entity;

        _attackController.Initialize(this);
    }

    public void Dispose()
    {
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

    public void SetAttackCommand(IEntityAttackStrategy strategy)
    {
        AddAttackCommand(strategy);
    }

    public void SetIdleCommand()
    {
        AddIdleCommand();
    }

    public void SetPositionToMove(Vector2 position)
    {
        _positionToMove = (Vector2)_character.Transform.position + position;
    }

    public ICommand GetCurrentCommand()
    {
        return _currentCommand = _currentCommand != null ? _currentCommand : null;
    }

    private void AddMoveCommand()
    {
        _currentCommand?.CancelCommand();

        _currentCommand = null;

        _currentCommand = new MoveCommand(_character, _positionToMove);

        _isMoving = true;

        ExecuteCommand();
    }

    private void AddAttackCommand(IEntityAttackStrategy strategy)
    {
        _currentCommand?.CancelCommand();

        _currentCommand = null;

        _currentCommand = new AttackCommand(_character, strategy, _coroutinePerformer);

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
