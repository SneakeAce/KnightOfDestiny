using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EntityConfig/EnemyConfig", fileName = "EnemyConfig")]
public class EnemyConfig : EntityConfig
{
    [field: SerializeField] public EnemyType EnemyType { get; private set; }
    [field: SerializeField] public int MinAmountCurrencyDropped { get; private set; }
    [field: SerializeField] public int MaxAmountCurrencyDropped { get; private set; }

    public override T GetConfig<T>()
    {
        T thisConfig = this as T;

        if (thisConfig == null)
            throw new InvalidCastException($"EnemyConfig / GetConfig<T> / thisConfig is not cast to {typeof(T)}");

        return thisConfig;
    }
}
