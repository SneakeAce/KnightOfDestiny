using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EntityConfig/EnemyLibraryConfigs", fileName = "EnemyLibraryConfigs")]
public class EnemyLibraryConfigs : LibraryConfigsBase
{
    [field: SerializeField] public List<EnemyConfig> EnemyConfigs { get; private set; }

    override public List<T> GetConfigs<T>()
    {
        var tempList = new List<T>();

        tempList = EnemyConfigs as List<T>;

        if (tempList == null)
            throw new InvalidCastException($"EnemyLibraryConfigs / GetConfig<T> / tempList is not {typeof(List<T>)}!");

        return tempList;
    }
}
