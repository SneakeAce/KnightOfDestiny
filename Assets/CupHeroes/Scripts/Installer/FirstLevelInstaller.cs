using Unity.Cinemachine;
using UnityEngine;
using Zenject;

public class FirstLevelInstaller : MonoInstaller
{
    [SerializeField] private PlayerHUD _playerHUDPrefab;
    [SerializeField] private Camera _cameraPrefab;
    [SerializeField] private CinemachineCamera _cinemachineCameraPrefab;

    public override void InstallBindings()
    {
        BindCameraSpawner();

        BindCurrencySystem();

        BindPlayerHUD();

        BindBootstrap();
    }

    private void BindCameraSpawner()
    {
        Container.Bind<ICameraSpawner>()
            .To<CameraSpawner>()
            .AsSingle();

        Container.Bind<CameraController>()
            .AsSingle().WithArguments(_cinemachineCameraPrefab, _cameraPrefab);
    }

    private void BindBootstrap()
    {
        Container.Bind<FirstLevelBootstrap>()
            .AsSingle()
            .NonLazy();
    }

    private void BindCurrencySystem()
    {
        Container.Bind<CurrencyCounter>()
            .AsSingle();

        Container.Bind<CollectingCurrencyHandler>()
            .AsSingle();

        Container.Bind<ICurrencyController>()
            .To<CurrencyController>()
            .AsSingle();

        Container.Bind<CurrencyDisplayController>()
            .AsSingle();
    }

    private void BindPlayerHUD()
    {
        PlayerHUD playerHUD = Container.InstantiatePrefabForComponent<PlayerHUD>(_playerHUDPrefab);

        if (playerHUD == null)
        {
            Debug.LogError("PlayerHUD in FirstLevelInstaller is null!");
            return;
        }

        Container.Bind<PlayerHUD>()
            .FromInstance(playerHUD)
            .AsSingle();
    }
}
