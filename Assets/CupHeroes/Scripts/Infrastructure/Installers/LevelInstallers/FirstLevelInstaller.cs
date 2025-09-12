using Zenject;

public class FirstLevelInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindBootstrap();
        BindLevelMaanger();
    }

    private void BindBootstrap()
    {
        Container.Bind<ILevelBootstrapper>()
            .To<FirstLevelBootstrap>()
            .AsSingle()
            .NonLazy();
    }

    private void BindLevelMaanger()
    {
        Container.Bind<LevelController>()
            .AsSingle();
    }
}
