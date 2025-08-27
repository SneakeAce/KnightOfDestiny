using UnityEngine;

public interface IConfigsProvider
{
    void Initialize();
    LibraryConfigsBase GetLibraryConfig<T>() where T : ScriptableObject;
    SingleConfigBase GetSingleConfig<T>() where T : ScriptableObject;
    PoolsConfigBase GetPoolsConfig<T>() where T : ScriptableObject;
}
