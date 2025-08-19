using Zenject;

public class FirstLevelInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindBootstrap();
    }

    private void BindBootstrap()
    {
        Container.Bind<FirstLevelBootstrap>().AsSingle().NonLazy();
    }
}
