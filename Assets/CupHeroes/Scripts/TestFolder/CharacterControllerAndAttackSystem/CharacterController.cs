using System;
using UnityEngine;
using Zenject;

public class CharacterController : IEntityController, ITickable, IDisposable
{
    private IEntity _character;
    private IEnemy _currentTarget;

    private ICommandInvoker _commandInvoker;
    private ICommand _currentCommand;

    private TargetFinder _targetFinder;
    private CoroutinePerformer _coroutinePerformer;

    public CharacterController(ICommandInvoker commandInvoker, 
        CoroutinePerformer coroutinePerformer)
    {
        _commandInvoker = commandInvoker;
        _coroutinePerformer = coroutinePerformer;
    }

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
        return;
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

        _currentCommand = new MoveCommand(_character, new Vector2(2f, 0f));

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
