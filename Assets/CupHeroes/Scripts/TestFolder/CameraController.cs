using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController
{
    private ICameraSpawner _cameraSpawner;
    private IEntity _character;

    private CinemachineCamera _cinemachineCamera;
    private Camera _camera;

    private CinemachineCamera _cinemachineCameraPrefab;
    private Camera _cameraPrefab;

    public CameraController(ICameraSpawner cameraSpawner, IEntity character,
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
        if (_character is not Character character)
            throw new ArgumentNullException("Character in CameraController is null!");

        _camera = _cameraSpawner.GetWorldCamera();
        _cinemachineCamera = _cameraSpawner.GetCinemachineCamera();

        if (_cinemachineCamera == null)
        {
            Debug.LogError("cinemachineCamera in CameraController is null!");
            return;
        }

        _cinemachineCamera.Follow = character.transform;
    }
}
