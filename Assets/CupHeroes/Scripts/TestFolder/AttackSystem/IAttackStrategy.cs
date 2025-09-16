using System;
using System.Collections;

public interface IAttackStrategy
{
    event Action OnAllTargetsDestroyed;

    void Initialize(AttackState state);
    void SubscribingEvents();
    void UnsubscribingEvents();
    IEnumerator AttackJob();
}
