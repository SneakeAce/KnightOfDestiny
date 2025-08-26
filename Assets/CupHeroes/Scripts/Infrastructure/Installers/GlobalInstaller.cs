using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private ConfigsContainer _configsContainer;

    public override void InstallBindings()
    {
        BindConfigsProvider();

        BindPoolsManager();

        BindGlobalBootstrapper();
    }

    private void BindConfigsProvider()
    {
        Container.Bind<IConfigsProvider>()
            .To<ConfigsProvider>()
            .AsSingle()
            .WithArguments(_configsContainer);
    }

    private void BindPoolsManager()
    {
        Container.Bind<IPoolsFactory>()
            .To<EnemyPoolsFactory>()
            .AsSingle();

        Container.Bind<IPoolsManager>()
            .To<PoolsManager>()
            .AsSingle();
    }

    private void BindGlobalBootstrapper()
    {
        Container.Bind<GlobalBootstrapper>()
            .AsSingle()
            .NonLazy();
    }
}
