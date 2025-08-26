using System;
using System.Collections.Generic;
using UnityEngine;

public class ConfigsProvider : IConfigsProvider
{
    private IConfigsContainer _configsContainer;

    private Dictionary<Type, object> _librariesConfigs = new Dictionary<Type, object>();
    private Dictionary<Type, object> _singleConfigs = new Dictionary<Type, object>();
    private Dictionary<Type, object> _poolsConfigs = new Dictionary<Type, object>();

    public ConfigsProvider(IConfigsContainer configsContainer)
    {
        _configsContainer = configsContainer;
    }

    public void Initialize()
    {
        Debug.Log("ConfigsProvider Initialize");

        _librariesConfigs = LibraryConfigsCaching();
        _singleConfigs = SingleConfigsCaching();
        _poolsConfigs = PoolsConfigsCaching();
    }

    public LibraryConfigsBase GetLibraryConfig<T>() where T : ScriptableObject
    {
        if (_librariesConfigs.TryGetValue(typeof(T), out var libraryConfig))
        {
            if (libraryConfig is LibraryConfigsBase libConfig)
                return libConfig;

            throw new InvalidCastException($"Library for type {typeof(T)} does not implement ILibraryConfigs.");
        }

        throw new KeyNotFoundException($"No config library found for type {typeof(T)}.");
    }    
    
    public SingleConfigBase GetSingleConfig<T>() where T : ScriptableObject
    {
        if (_singleConfigs.TryGetValue(typeof(T), out var singleConfig))
        {
            if (singleConfig is SingleConfigBase singConfig)
                return singConfig;

            throw new InvalidCastException($"Config for type {typeof(T)} does not implement ISingleConfig.");
        }

        throw new KeyNotFoundException($"No Config found for type {typeof(T)}.");
    }

    public PoolsConfigBase GetPoolsConfig<T>() where T : ScriptableObject
    {
        if (_poolsConfigs.TryGetValue(typeof(T), out var poolsConfig))
        {
            if (poolsConfig is PoolsConfigBase poolConfig)
                return poolConfig;

            throw new InvalidCastException($"Library for type {typeof(T)} does not implement ILibraryConfigs.");
        }

        throw new KeyNotFoundException($"No config library found for type {typeof(T)}.");
    }

    private Dictionary<Type, object> LibraryConfigsCaching()
    {
        Dictionary<Type, object> tempDict = new Dictionary<Type, object>();

        for (int i = 0; i < _configsContainer.LibraryConfigs.Count; i++)
        {
            var currentLib = _configsContainer.LibraryConfigs[i];
            var currentLibType = currentLib.GetType();

            if (currentLib is not LibraryConfigsBase)
                throw new InvalidCastException("Object is not LibraryConfigsBase!");

            if (tempDict.ContainsKey(currentLibType))
                continue;

            tempDict[currentLibType] = currentLib;
        }

        return tempDict;
    }

    private Dictionary<Type, object> SingleConfigsCaching()
    {
        Dictionary<Type, object> tempDict = new Dictionary<Type, object>();

        for (int i = 0; i < _configsContainer.SingleConfigs.Count; i++)
        {
            var currentConfig = _configsContainer.SingleConfigs[i];
            var currentConfigType = currentConfig.GetType();

            if (typeof(SingleConfigBase).IsAssignableFrom(currentConfigType) == false)
                throw new InvalidCastException("Object is not SingleConfigBase!");

            if (tempDict.ContainsKey(currentConfigType))
                continue;

            tempDict[currentConfigType] = currentConfig;
        }

        return tempDict;
    }

    private Dictionary<Type, object> PoolsConfigsCaching()
    {
        Dictionary<Type, object> tempDict = new Dictionary<Type, object>();

        for (int i = 0; i < _configsContainer.PoolsConfigs.Count; i++)
        {
            var currentConfig = _configsContainer.PoolsConfigs[i];
            var currentConfigType = currentConfig.GetType();

            if (typeof(PoolsConfigBase).IsAssignableFrom(currentConfigType) == false)
                throw new InvalidCastException("Object is not SingleConfigBase!");

            if (tempDict.ContainsKey(currentConfigType))
                continue;

            tempDict[currentConfigType] = currentConfig;
        }

        return tempDict;
    }
}
