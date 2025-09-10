public interface IEntityController
{
    void Initialize(IEntity entity);
    void SetMoveCommand();
    void SetAttackCommand();
    void SetIdleCommand();

}
