public interface IUIElement
{
    void Initialize();
    void SetPool(IObjectPool pool);
    void ReturnToPool();
}
