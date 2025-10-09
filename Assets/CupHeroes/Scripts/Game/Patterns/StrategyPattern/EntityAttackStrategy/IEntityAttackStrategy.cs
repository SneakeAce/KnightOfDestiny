using System.Collections.Generic;

public interface IEntityAttackStrategy : IAttackStrategy
{
    void Initialize(AttackState state);

    void SwitchTarget(IEntity newTarget);
    void GetTargets(List<IEntity> targets);
}
