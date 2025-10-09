using System;
using System.Collections;

public interface IAttackStrategy : IStrategy, IDisposable
{
    event Action OnAllTargetsDestroyed;

    IEnumerator AttackJob();
}
