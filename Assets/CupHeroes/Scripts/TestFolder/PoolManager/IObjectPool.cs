using UnityEngine;

public interface IObjectPool
{
    void CreatePool();
    Object GetObjectFromPool();
    void ReturnPoolObject(Object poolObject);

}
