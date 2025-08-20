using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraSpawner : ICameraSpawner
{
    private CinemachineCamera _cinemachineCameraPrefab;
    private Camera _cameraPrefab;

    public void Initialize(CinemachineCamera cinemachineCameraPrefab, Camera cameraPrefab)
    {
        _cinemachineCameraPrefab = cinemachineCameraPrefab;
        _cameraPrefab = cameraPrefab;
    }

    public Camera GetWorldCamera()
    {
        return SpawnWorldCamera();
    }

    public CinemachineCamera GetCinemachineCamera()
    {
        return SpawnCinemachineCamera();
    }

    private Camera SpawnWorldCamera()
    {
        Camera camera = GameObject.Instantiate(_cameraPrefab,
            new Vector3(0f, 0f, 0f),
            Quaternion.identity);

        if (camera == null)
            throw new ArgumentNullException("Camera in CameraSpawner is null!");

        return camera;
    }

    private CinemachineCamera SpawnCinemachineCamera()
    {
        CinemachineCamera cinemachineCamera = GameObject.Instantiate(_cinemachineCameraPrefab, 
            new Vector3(0f, 0f, 0f), 
            Quaternion.identity);

        if (cinemachineCamera == null)
            throw new ArgumentNullException("CinemachineCamera in CameraSpawner is null!");

        return cinemachineCamera;
    }
}
