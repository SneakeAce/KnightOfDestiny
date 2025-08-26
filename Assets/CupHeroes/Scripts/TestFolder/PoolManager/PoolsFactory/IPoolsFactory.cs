using System.Collections.Generic;

public interface IPoolsFactory
{
    PoolType PoolType { get; }
    Dictionary<int, IObjectPool> CreatePools();
}
