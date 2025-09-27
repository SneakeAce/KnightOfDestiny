public interface IEntityController
{
    void Initialize(IEntity entity);
    void SetMoveCommand();
    void SetAttackCommand(IEntityAttackStrategy strategy);
    void SetIdleCommand();
    ICommand GetCurrentCommand();
}
