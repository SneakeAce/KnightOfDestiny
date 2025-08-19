using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/PoolsConfigs/EnemyPoolsConfig", fileName = "EnemyPoolsConfig")]
public class EnemyPoolsConfig : ScriptableObject
{
    [field: SerializeField] public List<PoolStats> Pools { get; private set; }
}
