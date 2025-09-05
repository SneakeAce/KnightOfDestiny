using System;
using Zenject;

public class EnemyController : ITickable, IDisposable
{
    private IEnemy _enemy;
    private IEnemyHealth _enemyHealth;
    private IEntity _target;

    private ICommandInvoker _commandInvoker;

    private CollectingCurrencyHandler _currencyHandler;

    public EnemyController(IEnemy enemy, ICommandInvoker commandInvoker, 
        Character target, CollectingCurrencyHandler currencyHandler)
    {
        _enemy = enemy;
        _commandInvoker = commandInvoker;
        _target = target;
        _currencyHandler = currencyHandler;
    }

    public void Dispose()
    {
        _enemyHealth.OnGiveAwayCurrency -= _currencyHandler.GetCurrency;
    }

    public void Initialize()
    {
        SubcrubingEvents();

        ExecuteMoveCommand();
    }

    public void Tick()
    {
        
    }

    public void SetMoveCommand()
    {
        ExecuteMoveCommand();
    }

    public void SetAttackCommand()
    {
        ExecuteAttackCommand();
    }

    public void SetIdleCommand()
    {
        ExecuteIdleCommand();
    }

    private void SubcrubingEvents()
    {
        _enemyHealth = _enemy.Health as IEnemyHealth;

        _enemyHealth.OnGiveAwayCurrency += _currencyHandler.GetCurrency;
    }

    private void ExecuteMoveCommand()
    {
        _commandInvoker.AddCommand(new MoveCommand(_enemy, _target.Transform.position));
        _commandInvoker.ExecuteCommand();
    }

    private void ExecuteAttackCommand()
    {
        return;
    }

    private void ExecuteIdleCommand() 
    {
        return;
    }
}
