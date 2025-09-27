public interface IAttackCommand : ICommand
{
    void SwitchState(IEntityAttackStrategy strategy);
}
