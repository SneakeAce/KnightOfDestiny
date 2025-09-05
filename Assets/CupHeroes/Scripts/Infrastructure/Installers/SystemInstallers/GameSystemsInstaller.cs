using Zenject;

public class GameSystemsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindCameraSpawner();

        BindEnemySpawnerSystem();

        BindCurrencySystem();

        BindTickUpdater();
    }

    private void BindCameraSpawner()
    {
        Container.Bind<ICameraSpawner>()
            .To<CamerasSpawner>()
            .AsSingle();

        Container.Bind<CamerasController>()
            .AsSingle();
    }

    private void BindEnemySpawnerSystem()
    {
        Container.Bind<IEnemyControllersFactory>()
            .To<EnemyControllersFactory>()
            .AsSingle();

        Container.Bind<IEnemyFactory>()
            .To<EnemyFactory>()
            .AsSingle();

        Container.Bind<EnemySpawner>()
        .AsSingle();

        Container.Bind<EnemyWaveController>()
        .AsSingle();
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

    private void BindTickUpdater()
    {
        Container.BindInterfacesAndSelfTo<TickUpdater>()
            .AsSingle();
    }
}
