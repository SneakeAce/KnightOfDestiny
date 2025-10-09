using Zenject;

public class StatsSystemInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindStatsControllers();

        BindStatsManager();
    }

    private void BindStatsControllers()
    {
        Container.Bind<IHealthStatsController>()
            .To<HealthStatsController>()
            .AsTransient();

        Container.Bind<IAttackStatsController>()
            .To<AttackStatsController>()
            .AsTransient();
    }

    private void BindStatsManager()
    {
        Container.Bind<IEntityStatsManager>()
            .To<EntityStatsManager>()
            .AsTransient();
    }
}
