using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/PoolsConfigs/UpgradeCardPoolsConfig", fileName = "UpgradeCardPoolsConfig")]
public class UpgradeCardPoolsConfig : PoolsConfigBase
{
    [field: SerializeField] public List<UpgradeCardPoolStats> PoolsStats { get; private set; }

    public override List<T> GetConfigs<T>()
    {
        return PoolsStats as List<T>;
    }
}
