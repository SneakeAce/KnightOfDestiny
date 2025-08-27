using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/UIConfigs/UILibraryConfigs", fileName = "UILibraryConfigs")]
public class UILibraryConfigs : LibraryConfigsBase
{
    [field: SerializeField] public List<UIConfig> Configs { get; private set; }

    public override List<T> GetConfigs<T>()
    {
        var tempList = new List<T>();

        tempList = Configs as List<T>;

        if (tempList == null)
            throw new InvalidCastException($"EnemyLibraryConfigs / GetConfig<T> / tempList is not {typeof(List<T>)}!");

        return tempList;
    }
}
