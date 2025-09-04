using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/PoolsConfigs/EnemyPoolsConfig", fileName = "EnemyPoolsConfig")]
public class EnemyPoolsConfig : PoolsConfigBase
{
    [field: SerializeField] public List<EnemyPoolStats> PoolsStats { get; private set; }

    public override List<T> GetConfigs<T>()
    {
        return PoolsStats as List<T>;
    }
}
