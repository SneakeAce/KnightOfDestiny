using System;

public interface IPoolsManager
{
    void Initialize();

    IObjectPool GetPool<TEnum>(PoolType poolType, TEnum type) where TEnum : Enum;
}
