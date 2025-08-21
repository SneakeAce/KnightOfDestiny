using System;
using UnityEngine;

public struct UISPawnerData
{
    public UISPawnerData(GameObject prefab, Vector2 spawnPosition, Quaternion spawnRotation)
    {
        Prefab = prefab;
        SpawnPosition = spawnPosition;
        SpawnRotation = spawnRotation;
    }

    public GameObject Prefab { get; }
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

    public T SpawnObject<T>(UISPawnerData spawnerData) where T : MonoBehaviour
    {
        T obj = _factory.CreateObject<T>(spawnerData.Prefab);

        if (obj == null)
            throw new ArgumentNullException("obj in UISpawner is null!");

        obj.transform.position = spawnerData.SpawnPosition;
        obj.transform.rotation = spawnerData.SpawnRotation;

        return obj;
    }

}
