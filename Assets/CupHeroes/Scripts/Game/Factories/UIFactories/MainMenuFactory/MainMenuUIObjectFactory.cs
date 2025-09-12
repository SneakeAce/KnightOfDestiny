using System;
using UnityEngine;
using Zenject;

public class MainMenuUIObjectFactory : IMainMenuUIObjectFactory
{
    private DiContainer _container;

    public MainMenuUIObjectFactory(DiContainer container)
    {
        _container = container;
    }

    public T CreateObject<T>(GameObject prefab) where T : UIElement
    {
        T prefabT = prefab.GetComponent<T>();

        if (prefabT == null)
            throw new ArgumentNullException($"This {nameof(prefabT)} is null!");

        T prefabInstance = GameObject.Instantiate(prefabT); 

        if (prefabInstance == null)
            throw new ArgumentNullException($"This {nameof(prefabInstance)} is null!");

        BindObject<T>(prefabInstance);

        return prefabInstance;
    }

    private void BindObject<T>(T prefabInstance) where T : class
    {
        _container.Bind<T>()
            .FromInstance(prefabInstance)
            .AsSingle();
    } 
}
