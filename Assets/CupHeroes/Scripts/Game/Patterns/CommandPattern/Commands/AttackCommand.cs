public class AttackCommand : ICommand
{
    private IEntity _entity;

    private IAttackStrategy _strategy;

    private CoroutinePerformer _performer;

    public AttackCommand(IEntity entity, IAttackStrategy strategy, CoroutinePerformer performer)
    {
        _entity = entity;
        _strategy = strategy;
        _performer = performer;
    }

    public void Execute()
    {
        _entity.StateMachine.SetState(new AttackState(_entity, _performer, _strategy));
    }

    public void CancelCommand()
    {
        _entity.StateMachine.RemoveState();
    }
}
