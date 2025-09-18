using System;
using Zenject;

public class CharacterAttackController : ITickable, IDisposable
{
    private IEntity _character;

    private ICommandInvoker _commandInvoker;
    private ICommand _currentCommand;

    private IAttackStrategy _currentStrategy;
    private ITargetFinderStrategy _targetFinderStrategy;

    private TargetFinderContext _targetFinderContext;
    private AttackTypeSwitcher _attackTypeSwitcher;

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public void Tick()
    {
        throw new NotImplementedException();
    }

    public void Initialize()
    {

    }

    public void SwitchAttackStrategy()
    {

    }

    private void InitializeTargetFinder()
    {

    }

    private ITargetFinderStrategy GetTargetFinderStrategy()
    {
        return null;
    }

    private IAttackStrategy GetAttackStrategy()
    {
        return null;
    }

    private void SetAttackCommand()
    {

    }

    private void ExecuteAttackCommand()
    {

    }
}
