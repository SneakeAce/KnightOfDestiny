using System;
using UnityEngine;

[Serializable]
public class UIPoolStats : PoolStats
{
    [field: SerializeField] public UIElementType Type { get; private set; }
}
