using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EntityConfigs/EnemyLibraryConfigs", fileName = "EnemyLibraryConfigs")]
public class EnemyLibraryConfigs : ScriptableObject
{
    [field: SerializeField] public List<EnemyConfig> EnemyConfigs { get; private set; }
}
