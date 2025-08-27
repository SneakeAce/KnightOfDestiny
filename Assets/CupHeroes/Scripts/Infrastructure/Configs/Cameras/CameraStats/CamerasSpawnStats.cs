using System;
using UnityEngine;

[Serializable]
public class CamerasSpawnStats
{
    [field: SerializeField] public CameraStats CameraStats { get; private set; }
    [field: SerializeField] public CinemachineCameraStats CmCameraStats { get; private set; }
}
