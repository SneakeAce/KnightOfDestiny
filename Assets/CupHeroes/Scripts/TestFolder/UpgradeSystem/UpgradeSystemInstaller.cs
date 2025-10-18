using UnityEngine;
using Zenject;

public class UpgradeSystemInstaller : MonoInstaller
{
    [SerializeField] private UpgradeCardsView _viewPrefab;

    public override void InstallBindings()
    {
        BindSpawnCardSystem();

        BindViews();

        BindUpgradeSystemController();
    }

    private void BindSpawnCardSystem()
    {
        Container.Bind<IUpgradeCardsFactory>()
            .To<UpgradeCardsFactory>()
            .AsSingle();

        Container.Bind<UpgradeCardsSpawner>()
            .AsSingle();
    }

    private void BindViews()
    {
        UpgradeCardsView view = Container.InstantiatePrefabForComponent<UpgradeCardsView>(
            _viewPrefab, 
            Vector3.zero, 
            Quaternion.identity,
            null
            );

        Container.Bind<UpgradeCardsView>()
            .FromInstance(view)
            .AsSingle();

        Container.Bind<UpgradeCardsViewController>()
            .AsSingle();
    }

    private void BindUpgradeSystemController()
    {
        Container.Bind<UpgradeSystemController>()
            .AsSingle();
    }
}
