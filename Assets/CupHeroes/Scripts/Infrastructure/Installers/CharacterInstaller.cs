using Zenject;

public class CharacterInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindCharacter();
        BindCharacterController();
    }

    private void BindCharacter()
    {
        Container.Bind<CharacterSpawner>()
            .AsSingle();
    }

    private void BindCharacterController()
    {
        Container.BindInterfacesAndSelfTo<CharacterAttackController>()
            .AsSingle();

        Container.BindInterfacesAndSelfTo<CharacterController>()
            .AsSingle();
    }
}
