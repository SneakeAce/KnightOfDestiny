public interface IAttackCommand : ICommand
{
    void UpdateState(IEntityAttackStrategy strategy);
    void RestartStrategyAtState();
}
