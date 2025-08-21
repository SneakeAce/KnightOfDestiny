using Unity.Cinemachine;
using UnityEngine;

public class CamerasController
{
    private IConfigsProvider _configsProvider;
    private CamerasSpawnConfig _camerasConfig;

    private ICameraSpawner _cameraSpawner;

    private Camera _camera;
    private Camera _cameraPrefab;
    private Vector2 _spawnPositionCamera;
    private Quaternion _spawnRotationCamera;

    private CinemachineCamera _cinemachineCamera;
    private CinemachineCamera _cinemachineCameraPrefab;
    private Vector2 _spawnPositionCmCamera;
    private Quaternion _spawnRotationCmCamera;

    public CamerasController(ICameraSpawner cameraSpawner, IConfigsProvider configsProvider)
    {
        _cameraSpawner = cameraSpawner;
        _configsProvider = configsProvider;
    }

    public Camera Camera => _camera;

    public void Initialize()
    {
        _camerasConfig = _configsProvider.GetSingleConfig<CamerasSpawnConfig>().GetConfig<CamerasSpawnConfig>();

        SetParameters();
        SpawnCamera();
    }

    public void SetTargetForCamera(IEntity entity) => _cinemachineCamera.Follow = entity.Transform;

    private void SetParameters()
    {
        _cameraPrefab = _camerasConfig.CamerasStats.CameraStats.CameraPrefab;
        _spawnPositionCamera = _camerasConfig.CamerasStats.CameraStats.CameraSpawnPosition;
        _spawnRotationCamera = _camerasConfig.CamerasStats.CameraStats.CameraSpawnRotation;

        _cinemachineCameraPrefab = _camerasConfig.CamerasStats.CmCameraStats.CmCameraPrefab;
        _spawnPositionCmCamera = _camerasConfig.CamerasStats.CmCameraStats.CmCameraSpawnPosition;
        _spawnRotationCmCamera = _camerasConfig.CamerasStats.CmCameraStats.CmCameraSpawnRotation;
    }

    private void SpawnCamera()
    {
        CameraSpawnData cameraData = new CameraSpawnData(
            _cameraPrefab, 
            _spawnPositionCamera, 
            _spawnRotationCamera
            );

        _camera = _cameraSpawner.SpawnWorldCamera(cameraData);

        CinemachineCameraSpawnData cmCameraData = new CinemachineCameraSpawnData(
            _cinemachineCameraPrefab,
            _spawnPositionCmCamera,
            _spawnRotationCmCamera
            );

        _cinemachineCamera = _cameraSpawner.SpawnCinemachineCamera(cmCameraData);

        if (_cinemachineCamera == null)
        {
            Debug.LogError("cinemachineCamera in CameraController is null!");
            return;
        }
    }
}
