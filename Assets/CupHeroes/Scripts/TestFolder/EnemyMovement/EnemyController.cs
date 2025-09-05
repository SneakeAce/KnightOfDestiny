using System;
using UnityEngine;
using Zenject;

public class EnemyController : ITickable, IDisposable
{
    private IEnemy _enemy;
    private IEnemyHealth _enemyHealth;
    private IEntity _target;

    private ICommandInvoker _commandInvoker;
    private ICommand _currentCommand;

    private CollectingCurrencyHandler _currencyHandler;
    private CoroutinePerformer _coroutinePerformer;

    private float _minDistanceToTarget;

    private bool _enemyNearTarget;

    public EnemyController(IEnemy enemy, ICommandInvoker commandInvoker, 
        Character target, CollectingCurrencyHandler currencyHandler, CoroutinePerformer coroutinePerformer)
    {
        _enemy = enemy;
        _commandInvoker = commandInvoker;
        _target = target;
        _currencyHandler = currencyHandler;
        _coroutinePerformer = coroutinePerformer;
    }

    public void Dispose()
    {
        _enemyHealth.OnGiveAwayCurrency -= _currencyHandler.GetCurrency;
    }

    public void Initialize()
    {
        _minDistanceToTarget = _enemy.Config.AttackStats.AttackRange;

        SubcrubingEvents();

        SetMoveCommand();
    }

    public void Tick()
    {
        if (Vector2.Distance(_enemy.Transform.position, _target.Transform.position) <= _minDistanceToTarget
            && _enemyNearTarget == false)
        {
            _enemyNearTarget = true;
            SetAttackCommand();
        }

        if (Vector2.Distance(_enemy.Transform.position, _target.Transform.position) >= _minDistanceToTarget
            && _enemyNearTarget)
        {
            _enemyNearTarget = false;
            SetMoveCommand();
        }
    }

    public void SetMoveCommand()
    {
        _enemyNearTarget = false;

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

    private void SubcrubingEvents()
    {
        _enemyHealth = _enemy.Health as IEnemyHealth;

        _enemyHealth.OnGiveAwayCurrency += _currencyHandler.GetCurrency;
    }

    private void AddMoveCommand()
    {
        _currentCommand?.CancelCommand();

        _currentCommand = null;

        _currentCommand = new MoveCommand(_enemy, _target.Transform.position);

        ExecuteCommand();
    }

    private void AddAttackCommand()
    {
        _currentCommand?.CancelCommand();

        _currentCommand = null;

        _currentCommand = new AttackCommand(_enemy, _target, _coroutinePerformer);

        ExecuteCommand();
    }

    private void AddIdleCommand() 
    {
        _currentCommand?.CancelCommand();

        _currentCommand = null;

        _currentCommand = new IdleCommand(_enemy);

        ExecuteCommand();
    }

    private void ExecuteCommand()
    {
        _commandInvoker.AddCommand(_currentCommand);
        _currentCommand.Execute();
    }
}
