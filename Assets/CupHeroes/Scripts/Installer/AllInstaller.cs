using UnityEngine;
using Zenject;

public class AllInstaller : MonoInstaller
{
    [SerializeField] private EnemyLibraryConfigs _enemyConfigs;
    [SerializeField] private EnemyPoolsConfig _enemyPoolsConfig;
    [SerializeField] private WaveControllerConfig _waveControllerConfig;
    [SerializeField] private CharacterConfig _characterConfig;
    [SerializeField] private CoroutinePerformer _performerPrefab;

    public override void InstallBindings()
    {
        BindUtilities();

        BindEntityComponents();

        BindServices();

        BindEnemySpawnerSystem();

        BindCharacter();
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

    private void BindEntityComponents()
    {
        Container.Bind<IEntityHealth>()
            .To<EntityHealth>()
            .AsTransient();
    }

    private void BindServices()
    {
        Container.Bind<ICommandInvoker>()
            .To<CommandInvoker>()
            .AsSingle();
    }

    private void BindEnemySpawnerSystem()
    {
        Container.Bind<IEnemyPoolsFactory>()
            .To<EnemyPoolsFactory>()
            .AsSingle();

        Container.Bind<IEnemyPoolManager>()
            .To<EnemyPoolManager>()
            .AsSingle()
            .WithArguments(_enemyConfigs, _enemyPoolsConfig);

        Container.Bind<IEnemyFactory>()
            .To<EnemyFactory>()
            .AsSingle();

        Container.Bind<EnemySpawner>()
            .AsSingle();

        Container.Bind<EnemyWaveController>()
            .AsSingle()
            .WithArguments(_waveControllerConfig);
    }

    private void BindCharacter()
    {
        Container.Bind<CharacterConfig>().AsSingle();

        Character character = Container
            .InstantiatePrefabForComponent<Character>(
            _characterConfig.Prefab, 
            new Vector2(0f, 0f), 
            Quaternion.Euler(0f, 0f, 0f), 
            null);

        if (character == null)
            Debug.LogError("Character is null!");

        Container.BindInterfacesAndSelfTo<Character>()
            .FromInstance(character)
            .AsSingle();
    }
}
