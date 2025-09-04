using UnityEngine;

public abstract class UIElement : MonoBehaviour
{
    protected IObjectPool _pool;

    public void SetPool(IObjectPool pool) => _pool = pool;

    public void ReturnToPool()
    {
        _pool?.ReturnPoolObject(this);

        _pool = null;
    }
}
