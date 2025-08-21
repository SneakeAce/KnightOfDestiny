using UnityEngine;

public abstract class SingleConfigBase : ScriptableObject
{
    abstract public T GetConfig<T>() where T : ScriptableObject;
}
