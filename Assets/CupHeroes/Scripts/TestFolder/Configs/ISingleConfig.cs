using UnityEngine;

public interface ISingleConfig
{
    T GetConfig<T>() where T : ScriptableObject;
}
