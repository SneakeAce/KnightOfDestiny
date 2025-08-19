using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public struct PoolCreatingArguments
{
    public PoolCreatingArguments(int poolSize, bool canExpandPool, Transform container)
    {
        PoolSize = poolSize;
        CanExpandPool = canExpandPool;
        Container = container;
    }

    public int PoolSize { get; }
    public bool CanExpandPool { get; }
    public Transform Container { get; }
}

public class ObjectPool<TObject>
    where TObject : MonoBehaviour
{
    private const int CountPerSpawn = 1;

    private Queue<TObject> _availableObjects;
    private HashSet<TObject> _objectsInUse;

    private GameObject _prefab;

    private Transform _container;

    private int _countPoolObject;
    private int _poolSize;

    private bool _canExpandPool;

    public ObjectPool(PoolCreatingArguments poolArguments)
    {
        _poolSize = poolArguments.PoolSize;
        _canExpandPool = poolArguments.CanExpandPool;
        _container = poolArguments.Container;

        _availableObjects = new Queue<TObject>();
        _objectsInUse = new HashSet<TObject>();
    }

    public void CreatePool()
    {
        for (int i = 0; i < _poolSize; i++)
            CreatePoolObject();
    }

    public Object GetObjectFromPool()
    {
        return GetPoolObject();
    }

    public void ReturnPoolObject(Object poolObject)
    {
        ReturnObject(poolObject as TObject);
    }

    private TObject GetPoolObject()
    {
        TObject poolObject = GetObject();

        if (poolObject != null)
            return poolObject;

        if (_canExpandPool)
            return CreatePoolObject(_canExpandPool);

        return null;
    }

    private void ReturnObject(TObject poolObject)
    {
        if (_objectsInUse.Contains(poolObject) == false)
            return;

        SetActiveObject(poolObject, false);

        _objectsInUse.Remove(poolObject);
        _availableObjects.Enqueue(poolObject);
    }

    private TObject CreatePoolObject(bool isActiveByDefault = false)
    {
        var instancePoolObject = Object.Instantiate(_prefab, _container);
        var poolObject = instancePoolObject.GetComponent<TObject>();

        _countPoolObject += CountPerSpawn;
        instancePoolObject.name = _prefab.name + _countPoolObject.ToString();

        SetActiveObject(poolObject, isActiveByDefault);

        _availableObjects.Enqueue(poolObject);

        return poolObject;
    }

    private TObject GetObject()
    {
        if (_availableObjects.Count == 0)
            return null;

        TObject poolObject = _availableObjects.Dequeue();

        _objectsInUse.Add(poolObject);

        SetActiveObject(poolObject, true);

        return poolObject;
    }

    private void SetActiveObject(TObject poolObject, bool value)
    {
        switch (poolObject)
        {
            case GameObject go:
                go.SetActive(value);
                break;
            case Component comp:
                comp.gameObject.SetActive(value);
                break;
            default:
                throw new InvalidOperationException("TObject должен быть GameObject или Component");
        }
    }

}