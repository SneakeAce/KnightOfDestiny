using UnityEngine;

public interface IMainMenuUIObjectFactory
{
    T CreateObject<T>(GameObject prefab) where T : UIElement;
}
