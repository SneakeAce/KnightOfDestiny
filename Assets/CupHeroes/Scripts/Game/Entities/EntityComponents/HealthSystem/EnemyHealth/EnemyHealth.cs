using System;
using UnityEngine;

public class EnemyHealth : EntityHealth, IEnemyHealth
{
    private EnemyConfig _enemyConfig;

    public event Action<int> OnGiveAwayCurrency;

    public void SetConfig(EnemyConfig config) => _enemyConfig = config;

    public override void Initialize(IEntity entity, float initialHealth)
    {
        base.Initialize(entity, initialHealth);

        EntityDied += GiveAwayCurrency;
    }

    private void GiveAwayCurrency(IEntity enemy)
    {
        int currencyToGive = UnityEngine.Random.Range(_enemyConfig.MinAmountCurrencyDropped,
            _enemyConfig.MaxAmountCurrencyDropped);

        Debug.Log($"EnemyHealth. currencyToGive = {currencyToGive}");
        OnGiveAwayCurrency?.Invoke(currencyToGive);
    }
}
