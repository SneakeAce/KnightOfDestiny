using System;
using UnityEngine;
using Zenject;

public class UIFactory : IUIFactory
{
    public T CreateObject<T>(GameObject prefab) where T : MonoBehaviour
    {
        T objInstance = prefab.GetComponent<T>();

        if (objInstance == null)
            throw new ArgumentNullException("objInstance T is null in UIFactory");

        T obj = GameObject.Instantiate(objInstance);

        if (obj == null)
            throw new ArgumentNullException("obj T is null in UIFactory");

        return obj;
    }
}
