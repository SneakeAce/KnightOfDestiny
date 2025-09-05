using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/WaveControllerConfig", fileName = "WaveControllerConfig")]
public class WaveControllerConfig : SingleConfigBase
{
    [field: SerializeField] public WaveControllerStats WaveControllerStats { get; private set; }
    [field: SerializeField] public StartPositionStats StartPositionStats { get; private set; }

    public override T GetConfig<T>()
    {
        T thisConfig = this as T;

        if (thisConfig == null)
            throw new InvalidCastException($"WaveControllerConfig / GetConfig<T> / " +
                $"thisConfig is not cast to {typeof(T)}");

        return thisConfig;
    }
}
