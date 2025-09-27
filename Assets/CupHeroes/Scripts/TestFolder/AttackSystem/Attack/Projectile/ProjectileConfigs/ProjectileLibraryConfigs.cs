using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLibraryConfigs : LibraryConfigsBase
{
    [field: SerializeField] public List<ProjectileConfig> ProjectileConfigs { get; private set; }

    public override List<T> GetConfigs<T>()
    {
        var tempList = new List<T>();

        tempList = ProjectileConfigs as List<T>;

        if (tempList == null)
            throw new InvalidCastException($"ProjectileLibraryConfigs / GetConfig<T> / tempList is not {typeof(List<T>)}!");

        return tempList;
    }
}
