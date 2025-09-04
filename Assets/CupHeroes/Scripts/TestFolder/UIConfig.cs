using UnityEngine;

public abstract class UIConfig : ScriptableObject
{
    [field: SerializeField] public GameObject Prefab { get; private set; }

    public abstract int GetTypeId();

    public T GetConfig<T>() where T : ScriptableObject
    {
        return this as T;
    }

}
