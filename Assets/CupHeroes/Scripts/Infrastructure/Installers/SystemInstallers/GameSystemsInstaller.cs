using Unity.Cinemachine;
using UnityEngine;
using Zenject;

public class GameSystemsInstaller : MonoInstaller
{
    [Header("CameraPrefabs")]
    [SerializeField] private Camera _cameraPrefab;
    [SerializeField] private CinemachineCamera _cinemachineCameraPrefab;

    public override void InstallBindings()
    {
        BindCameraSpawner();

        BindEnemySpawnerSystem();

        BindCurrencySystem();
    }

    private void BindEnemySpawnerSystem()
    {
        //Container.Bind<IEnemyPoolsFactory>()
        //.To<EnemyPoolsFactory>()
        //.AsSingle();

        //Container.Bind<IEnemyPoolManager>()
        //    .To<EnemyPoolManager>()
        //    .AsSingle();

        //Container.Bind<IEnemyFactory>()
        //    .To<EnemyFactory>()
        //    .AsSingle();

        //Container.Bind<EnemySpawner>()
        //.AsSingle();

        //Container.Bind<EnemyWaveController>()
        //.AsSingle();
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

    private void BindCameraSpawner()
    {
        Container.Bind<ICameraSpawner>()
            .To<CamerasSpawner>()
            .AsSingle();

        Container.Bind<CamerasController>()
            .AsSingle();
    }
}
