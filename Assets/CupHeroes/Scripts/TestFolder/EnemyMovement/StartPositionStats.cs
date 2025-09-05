using System;
using UnityEngine;

[Serializable]
public class StartPositionStats
{
    [field: SerializeField] public float StartPositionByX { get; private set; }
    [field: SerializeField, Range(-20f, 0f)] public float MinStartPositionOffsetByX { get; private set; }
    [field: SerializeField, Range(0f, 20f)] public float MaxStartPositionOffsetByX { get; private set; }

    [field: SerializeField] public float StartPositionByY { get; private set; }
    [field: SerializeField, Range(-3f, 0f)] public float MinStartPositionOffsetByY { get; private set; }
    [field: SerializeField, Range(0f, 3f)] public float MaxStartPositionOffsetByY { get; private set; }

    [field: SerializeField] public Quaternion StartPositionRotation { get; private set; }
}
