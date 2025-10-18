using UnityEngine;

public abstract class UIElement : MonoBehaviour, IUIElement
{
    protected IObjectPool _pool;

    public abstract void Initialize();

    public void SetPool(IObjectPool pool) => _pool = pool;

    public void ReturnToPool()
    {
        _pool?.ReturnPoolObject(this);

        _pool = null;
    }
}
