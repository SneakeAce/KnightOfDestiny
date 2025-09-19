public interface IEntityController
{
    void Initialize(IEntity entity);
    void SetMoveCommand();
    void SetAttackCommand(IAttackStrategy strategy);
    void SetIdleCommand();
    ICommand GetCurrentCommand();
}
