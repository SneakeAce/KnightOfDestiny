using UnityEngine;
using Zenject;

public class ServiceInstaller : MonoInstaller
{
    [SerializeField] private CoroutinePerformer _performerPrefab;

    public override void InstallBindings()
    {
        BindUtilities();

        BindCommandPattern();

        BindEntityBuilder();
    }

    private void BindCommandPattern()
    {
        Container.Bind<ICommandInvoker>()
            .To<CommandInvoker>()
            .AsSingle();
    }

    private void BindUtilities()
    {
        CoroutinePerformer performer = Container
            .InstantiatePrefabForComponent<CoroutinePerformer>(_performerPrefab);

        if (performer == null)
            Debug.LogError("performer is null!");

        Container.Bind<CoroutinePerformer>()
            .FromInstance(performer)
            .AsSingle();
    }

    private void BindEntityBuilder()
    {
        Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
        Container.Bind<IUISpawner>().To<UISpawner>().AsSingle();

        Container.Bind<IEntityBuilder>().To<EntityBuilder>().AsSingle();
    }
}
