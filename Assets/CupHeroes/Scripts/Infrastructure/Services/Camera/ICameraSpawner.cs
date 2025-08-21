using Unity.Cinemachine;
using UnityEngine;

public interface ICameraSpawner
{
    void Initialize(CinemachineCamera cinemachineCameraPrefab, Camera cameraPrefab);
    CinemachineCamera GetCinemachineCamera();
    Camera GetWorldCamera();
}
