using System;
using UnityEngine;

public struct UISpawnerData
{
    public UISpawnerData(UIElementType elementType, Vector2 spawnPosition, Quaternion spawnRotation)
    {
        ElementType = elementType;
        SpawnPosition = spawnPosition;
        SpawnRotation = spawnRotation;
    }

    public UIElementType ElementType { get; }
    public Vector2 SpawnPosition { get; }
    public Quaternion SpawnRotation { get; }
}

public class UISpawner : IUISpawner
{
    private IUIFactory _factory;

    public UISpawner(IUIFactory factory)
    {
        _factory = factory;
    }

    public UIElement SpawnObject(UISpawnerData spawnerData)
    {
        UIElement element = _factory.CreateObject(spawnerData.ElementType);

        if (element == null)
            throw new ArgumentNullException("obj in UISpawner is null!");

        element.transform.position = spawnerData.SpawnPosition;
        element.transform.rotation = spawnerData.SpawnRotation;

        return element;
    }

}
