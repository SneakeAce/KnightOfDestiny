using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private MainMenuSceneConfig _config;

    public override void InstallBindings()
    {
        CreateMainMenu();

        CreateLevelSelector();

        BindMainMenuBootstrapper();
    }

    private void CreateMainMenu()
    {
        Debug.Log("CreateMainMenu");

        MainMenuView mainMenuInstance = Container.InstantiatePrefabForComponent<MainMenuView>(_config.MainMenuPrefab);

        if (mainMenuInstance == null)
        {
            Debug.LogError($"{nameof(mainMenuInstance)} is null!");
            return;
        }

        mainMenuInstance.gameObject.SetActive(true);

        Container.Bind<MainMenuView>()
            .FromInstance(mainMenuInstance)
            .AsSingle();

        Container.Bind<MainMenuController>()
            .AsSingle();
    }

    private void CreateLevelSelector()
    {
        Debug.Log("CreateLevelSelector");

        LevelSelectorView levelSelectorInstance = Container.InstantiatePrefabForComponent<LevelSelectorView>(_config.LevelSelectorPrefab);

        if (levelSelectorInstance == null)
        {
            Debug.LogError($"{nameof(levelSelectorInstance)} is null!");
            return;
        }

        levelSelectorInstance.gameObject.SetActive(false);

        Container.Bind<LevelSelectorView>()
            .FromInstance(levelSelectorInstance)
            .AsSingle();

        Container.Bind<LevelSelectorController>()
            .AsSingle();
    }

    private void BindMainMenuBootstrapper()
    {
        Debug.Log("BindMainMenuBootstrapper");

        Container.Bind<ILevelBootstrapper>()
            .To<MainMenuBootstrapper>()
            .AsSingle()
            .NonLazy();
    }
}

