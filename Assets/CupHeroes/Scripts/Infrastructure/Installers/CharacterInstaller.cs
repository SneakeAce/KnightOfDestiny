using Zenject;

public class CharacterInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindCharacter();
    }

    private void BindCharacter()
    {
        Container.Bind<CharacterSpawner>()
            .AsSingle();
    }
}
