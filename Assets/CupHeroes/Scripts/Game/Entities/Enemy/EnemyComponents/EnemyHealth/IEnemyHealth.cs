using System;

public interface IEnemyHealth : IEntityHealth
{
    event Action<int> OnGiveAwayCurrency;

    void SetConfig(EnemyConfig config);
}
