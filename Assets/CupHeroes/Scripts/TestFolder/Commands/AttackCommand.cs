public class AttackCommand : ICommand
{
    private IEntity _entity;
    private IEntity _target;

    private CoroutinePerformer _performer;

    public AttackCommand(IEntity entity, IEntity target, CoroutinePerformer performer)
    {
        _entity = entity;
        _target = target;
        _performer = performer;
    }

    public void Execute()
    {
        _entity.StateMachine.SetState(new AttackState(_entity, _target, _performer));
    }

    public void CancelCommand()
    {
        _entity.StateMachine.RemoveState();
    }
}
