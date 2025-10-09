using Zenject;

public class ProjectileInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindProjectileFactories();

        BindProjectileSpawner();
    }

    private void BindProjectileFactories()
    {
        Container.Bind<IProjectileControllersFactory>()
            .To<ProjectileControllersFactory>()
            .AsSingle();

        Container.Bind<IProjectileFactory>()
            .To<ProjectileFactory>()
            .AsSingle();
    }

    private void BindProjectileSpawner()
    {
        Container.Bind<ProjectileSpawner>()
            .AsSingle();
    }

}
