using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController
{
    private ICameraSpawner _cameraSpawner;
    private Character _character;

    private CinemachineCamera _cinemachineCamera;
    private Camera _camera;

    private CinemachineCamera _cinemachineCameraPrefab;
    private Camera _cameraPrefab;

    public CameraController(ICameraSpawner cameraSpawner, Character character,
        CinemachineCamera cinemachineCameraPrefab, Camera cameraPrefab)
    {
        _cameraSpawner = cameraSpawner;
        _character = character;

        _cinemachineCameraPrefab = cinemachineCameraPrefab;
        _cameraPrefab = cameraPrefab;
    }

    public Camera Camera => _camera;

    public void Initialize()
    {
        _cameraSpawner.Initialize(_cinemachineCameraPrefab, _cameraPrefab);

        SpawnCamera();
    }

    private void SpawnCamera()
    {
        _camera = _cameraSpawner.GetWorldCamera();
        _cinemachineCamera = _cameraSpawner.GetCinemachineCamera();

        if (_cinemachineCamera == null)
        {
            Debug.LogError("cinemachineCamera in CameraController is null!");
            return;
        }

        _cinemachineCamera.Follow = _character.transform;
    }
}
