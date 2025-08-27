using System;
using UnityEngine;

[Serializable]
public class CameraStats
{
    [field: SerializeField] public Camera CameraPrefab { get; private set; }
    [field: SerializeField] public Vector2 CameraSpawnPosition { get; private set; }
    [field: SerializeField] public Quaternion CameraSpawnRotation { get; private set; }
}
