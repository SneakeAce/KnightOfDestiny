using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/UIConfigs/HealthBarConfig", fileName = "HealthBarConfig")]
public class HealthBarConfig : SingleConfigBase
{
    [field: SerializeField] public GameObject Prefab { get; private set; }

    public override T GetConfig<T>()
    {
        T thisConfig = this as T;

        if (thisConfig == null)
            throw new InvalidCastException($"HealthBarConfig / GetConfig<T> / thisConfig is not cast to {typeof(T)}");

        return thisConfig;
    }
}
