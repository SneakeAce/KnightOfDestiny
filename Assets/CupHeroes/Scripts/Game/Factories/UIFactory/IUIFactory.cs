using UnityEngine;

public interface IUIFactory
{
    T CreateObject<T>(GameObject prefab) where T : MonoBehaviour;
}
