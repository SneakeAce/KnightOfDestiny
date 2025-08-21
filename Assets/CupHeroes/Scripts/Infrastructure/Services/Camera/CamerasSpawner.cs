using System;
using Unity.Cinemachine;
using UnityEngine;

public struct CameraSpawnData
{
    public CameraSpawnData(Camera prefab, Vector2 spawnPosition, Quaternion spawnRotation)
    {
        Prefab = prefab;
        SpawnPosition = spawnPosition;
        SpawnRotation = spawnRotation;
    }

    public Camera Prefab { get; }
    public Vector2 SpawnPosition { get; }
    public Quaternion SpawnRotation { get; }
}

public struct CinemachineCameraSpawnData
{
    public CinemachineCameraSpawnData(CinemachineCamera prefab, Vector2 spawnPosition, Quaternion spawnRotation)
    {
        Prefab = prefab;
        SpawnPosition = spawnPosition;
        SpawnRotation = spawnRotation;
    }

    public CinemachineCamera Prefab { get; }
    public Vector2 SpawnPosition { get; }
    public Quaternion SpawnRotation { get; }
}

public class CamerasSpawner : ICameraSpawner
{
    public Camera SpawnWorldCamera(CameraSpawnData cameraData)
    {
        Camera camera = GameObject.Instantiate(
            cameraData.Prefab,
            cameraData.SpawnPosition,
            cameraData.SpawnRotation
            );
        

        if (camera == null)
            throw new ArgumentNullException("Camera in CameraSpawner is null!");

        return camera;
    }

    public CinemachineCamera SpawnCinemachineCamera(CinemachineCameraSpawnData cmCameraData)
    {
        CinemachineCamera cinemachineCamera = GameObject.Instantiate(
            cmCameraData.Prefab,
            cmCameraData.SpawnPosition,
            cmCameraData.SpawnRotation
            );

        if (cinemachineCamera == null)
            throw new ArgumentNullException("CinemachineCamera in CameraSpawner is null!");

        return cinemachineCamera;
    }
}
