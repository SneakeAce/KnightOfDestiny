public interface IEntityStateMachine
{
    void SetState(IEntityState state);
    void RemoveState();
    IEntityState GetCurrentState();
}
