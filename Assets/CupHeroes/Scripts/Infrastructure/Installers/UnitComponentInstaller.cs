using Zenject;

public class UnitComponentInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindEntityComponents();
    }

    private void BindEntityComponents()
    {
        Container.Bind<IEntityHealth>()
            .To<EntityHealth>()
            .AsTransient();
    }

}
