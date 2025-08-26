using System.Collections.Generic;
using UnityEngine;

public abstract class PoolsConfigBase : ScriptableObject
{
    [field: SerializeField] public PoolType PoolType { get; private set; }

    public abstract List<T> GetConfigs<T>() where T : ScriptableObject;
}
