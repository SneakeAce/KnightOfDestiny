public interface IAttackCommand : ICommand
{
    void SwitchStrategy(IEntityAttackStrategy strategy);
    void RestartStrategy();
}
