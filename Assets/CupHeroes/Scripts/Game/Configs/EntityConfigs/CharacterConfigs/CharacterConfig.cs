using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EntityConfig/CharacterConfig", fileName = "CharacterConfig")]
public class CharacterConfig : EntityConfig
{
    [field: SerializeField] public Vector3 SpawnPosition { get; private set; }
    [field: SerializeField] public Quaternion SpawnRotation { get; private set; }

    override public T GetConfig<T>()
    {
        T thisConfig = this as T;

        if (thisConfig == null)
            throw new InvalidCastException($"CharacterConfig / GetConfig<T> / thisConfig is not cast to {typeof(T)}");

        return thisConfig;
    }
}
