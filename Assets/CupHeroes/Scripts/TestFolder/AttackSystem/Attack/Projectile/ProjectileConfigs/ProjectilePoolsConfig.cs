using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/PoolsConfigs/ProjectilePoolsConfig", fileName = "ProjectilePoolsConfig")]
public class ProjectilePoolsConfig : PoolsConfigBase
{
    [field: SerializeField] public List<ProjectilePoolStats> PoolsStats { get; private set; }

    public override List<T> GetConfigs<T>()
    {
        return PoolsStats as List<T>;
    }
}
