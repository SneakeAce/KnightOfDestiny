using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/PoolsConfigs/UIPoolsConfig", fileName = "UIPoolsConfig")]
public class UIPoolsConfig : PoolsConfigBase
{
    [field: SerializeField] public List<UIPoolStats> PoolStats { get; private set; }

    public override List<T> GetConfigs<T>()
    {
        return PoolStats as List<T>;
    }
}
