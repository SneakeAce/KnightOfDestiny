public interface IAttackCommand : ICommand
{
    void SwitchState(IAttackStrategy strategy);
}
