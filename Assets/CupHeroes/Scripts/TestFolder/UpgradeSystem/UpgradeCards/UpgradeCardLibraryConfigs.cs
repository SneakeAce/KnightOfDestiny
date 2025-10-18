using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EntityConfig/UpgradeCardLibraryConfigs", fileName = "UpgradeCardLibraryConfigs")]
public class UpgradeCardLibraryConfigs : LibraryConfigsBase
{
    [field: SerializeField] public List<UpgradeCardConfig> UpgradeCardConfigs { get; private set; }

    override public List<T> GetConfigs<T>()
    {
        var tempList = new List<T>();

        tempList = UpgradeCardConfigs as List<T>;

        if (tempList == null)
            throw new InvalidCastException($"EnemyLibraryConfigs / GetConfig<T> / tempList is not {typeof(List<T>)}!");

        return tempList;
    }
}
