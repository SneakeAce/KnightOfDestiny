using System;
using UnityEngine;

[Serializable]
public class MultipliersValueCardStats
{
    [field: SerializeField] public RareCardType RareCardType { get; private set; }
    [field: SerializeField, Range(1f, 4f)] public float Multiplier { get; private set; }
}
