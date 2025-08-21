using Unity.Cinemachine;
using UnityEngine;

public interface ICameraSpawner
{ 
    Camera SpawnWorldCamera(CameraSpawnData cameraData);
    CinemachineCamera SpawnCinemachineCamera(CinemachineCameraSpawnData cmCameraData);
}
