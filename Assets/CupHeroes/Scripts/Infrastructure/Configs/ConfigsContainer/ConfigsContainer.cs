using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ConfigsContainer", fileName = "ConfigsContainer", order = 1)]
public class ConfigsContainer : ScriptableObject, IConfigsContainer
{
    [field: SerializeField] public List<LibraryConfigsBase> LibraryConfigs { get; private set; }
    [field: SerializeField] public List<SingleConfigBase> SingleConfigs { get; private set; }
    [field: SerializeField] public List<PoolsConfigBase> PoolsConfigs { get; private set; }
}
