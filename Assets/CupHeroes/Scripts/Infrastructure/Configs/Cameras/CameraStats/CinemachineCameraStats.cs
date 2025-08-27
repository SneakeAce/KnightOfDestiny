using System;
using Unity.Cinemachine;
using UnityEngine;

[Serializable]
public class CinemachineCameraStats
{
    [field: SerializeField] public CinemachineCamera CmCameraPrefab { get; private set; }
    [field: SerializeField] public Vector2 CmCameraSpawnPosition { get; private set; }
    [field: SerializeField] public Quaternion CmCameraSpawnRotation { get; private set; }
}
