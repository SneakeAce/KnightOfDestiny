using System;
using UnityEngine;

[Serializable]
public class PoolStats
{
    [field: SerializeField] public int MaxCountEntitiesInPool { get; private set; }
    [field: SerializeField] public bool PoolCanExpand { get; private set; }
}
