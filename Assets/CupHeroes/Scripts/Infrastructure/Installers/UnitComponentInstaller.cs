using Zenject;

public class UnitComponentInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindCharacterHealth();
        BindEnemyHealth();
    }


    private void BindEnemyHealth()
    {
        Container.Bind<IEnemyHealth>()
            .To<EnemyHealth>()
            .AsTransient();
    }

    private void BindCharacterHealth()
    {
        Container.Bind<ICharacterHealth>()
            .To<CharacterHealth>()
            .AsTransient();
    }
}
