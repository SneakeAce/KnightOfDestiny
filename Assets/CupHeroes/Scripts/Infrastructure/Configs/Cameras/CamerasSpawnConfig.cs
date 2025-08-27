using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/CameraSpawnConfig", fileName = "CameraSpawnConfig")]
public class CamerasSpawnConfig : SingleConfigBase
{
    [field: SerializeField] public CamerasSpawnStats CamerasStats { get; private set; }

    public override T GetConfig<T>()
    {
        T thisConfig = this as T;

        if (thisConfig == null)
            throw new InvalidCastException($"CameraSpawnConfig / GetConfig<T> / thisConfig is not cast to {typeof(T)}");

        return thisConfig;
    }
}
