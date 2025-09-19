public class AttackCommand : IAttackCommand
{
    private IEntity _entity;

    private IAttackStrategy _strategy;

    private CoroutinePerformer _performer;

    private AttackState _currentState;

    public AttackCommand(IEntity entity, IAttackStrategy strategy, CoroutinePerformer performer)
    {
        _entity = entity;
        _strategy = strategy;
        _performer = performer;
    }

    public void Execute()
    {
        _entity.StateMachine.SetState(new AttackState(_entity, _performer, _strategy));

        _currentState = (AttackState)_entity.StateMachine.GetCurrentState();
    }

    public void SwitchState(IAttackStrategy strategy)
    {
        _currentState?.UpdateStrategy(strategy);
    }

    public void CancelCommand()
    {
        _entity.StateMachine.RemoveState();
    }
}
