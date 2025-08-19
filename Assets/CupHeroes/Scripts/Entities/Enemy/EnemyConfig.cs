using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EntityConfig/EnemyConfig", fileName = "EnemyConfig")]
public class EnemyConfig : EntityConfig
{
    [field: SerializeField] public int MinAmountCurrencyDropped { get; private set; }
    [field: SerializeField] public int MaxAmountCurrencyDropped { get; private set; }
}
