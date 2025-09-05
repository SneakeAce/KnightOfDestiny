public class IdleCommand : ICommand
{
    private IEntity _entity;

    public IdleCommand(IEntity entity)
    {
        _entity = entity;
    }

    public void Execute()
    {
        _entity.StateMachine.SetState(new IdleState(_entity));
    }

    public void CancelCommand()
    {
        _entity.StateMachine.RemoveState();
    }
}
