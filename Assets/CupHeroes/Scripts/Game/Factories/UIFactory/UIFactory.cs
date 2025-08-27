using System.Collections.Generic;
using UnityEngine;

public class UIFactory : IUIFactory
{
    private IConfigsProvider _configsProvider;
    private IPoolsManager _poolsManager;

    private List<UIConfig> _libraryConfigs;
    private PoolType _poolType;

    public UIFactory(IConfigsProvider configsProvider, IPoolsManager poolsManager)
    {
        _configsProvider = configsProvider;
        _poolsManager = poolsManager;

        _libraryConfigs = _configsProvider.GetLibraryConfig<UILibraryConfigs>().GetConfigs<UIConfig>();
        _poolType = _configsProvider.GetPoolsConfig<UIPoolsConfig>().PoolType;
    }

    public UIElement CreateObject(UIElementType elementType)
    {
        UIConfig currentConfig = null;

        foreach (var config in _libraryConfigs)
        {
            if (config.GetTypeId() == (int)elementType)
            {
                currentConfig = config;
                break;
            }
            else
            {
                continue;
            }
        }

        if (currentConfig == null)
        {
            Debug.Log($"CurrentConfig in UIFactory is null!");
            return null;
        }

        var pool = _poolsManager.GetPool<UIElementType>(_poolType, elementType);

        if (pool == null)
        {
            Debug.Log("Pool in UIFactory is null!");
            return null;
        }

        UIElement element = (UIElement)pool.GetObjectFromPool();

        if (element == null)
        {
            Debug.Log("Element in UIFactory is null!");
            return null;
        }

        element.SetPool(pool);

        return element;
    }
}
