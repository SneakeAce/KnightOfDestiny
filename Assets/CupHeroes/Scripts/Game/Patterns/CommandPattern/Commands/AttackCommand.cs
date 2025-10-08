public class AttackCommand : IAttackCommand
{
    private IEntity _entity;

    private IEntityAttackStrategy _strategy;

    private CoroutinePerformer _performer;

    private AttackState _currentState;

    public AttackCommand(IEntity entity, IEntityAttackStrategy strategy, CoroutinePerformer performer)
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

    public void SwitchStrategy(IEntityAttackStrategy strategy)
    {
        _currentState?.SwitchStrategy(strategy);
    }

    public void RestartStrategy()
    {
        UnityEngine.Debug.Log($"RestartStrategy in {this.ToString()}");

        _currentState?.RestartStrategy();
    }

    public void CancelCommand()
    {
        _entity.StateMachine.RemoveState();
    }
}
