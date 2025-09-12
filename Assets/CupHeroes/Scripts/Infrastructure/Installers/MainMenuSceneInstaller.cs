using UnityEngine;
using Zenject;

public class MainMenuSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindFactory();

        BindControllersViews();

        BindMainMenuSceneManager();

        BindMainMenuBootstrapper();
    }

    private void BindFactory()
    {
        Container.Bind<IMainMenuUIObjectFactory>()
            .To<MainMenuUIObjectFactory>()
            .AsSingle();
    }

    private void BindControllersViews()
    {
        Container.Bind<MainMenuController>()
            .AsSingle();

        Container.Bind<LevelSelectorController>()
            .AsSingle();
    }

    private void BindMainMenuSceneManager()
    {
        Container.Bind<MainMenuSceneManager>()
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

