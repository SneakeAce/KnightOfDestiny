using System;

public class MainMenuBootstrapper : ILevelBootstrapper
{
    private MainMenuSceneManager _mainMenuSceneManager;

    public MainMenuBootstrapper(MainMenuSceneManager mainMenuSceneManager)
    {
        _mainMenuSceneManager = mainMenuSceneManager;

        Initialize();
    }

    public event Action OnInitialized;

    public void Initialize()
    {
        _mainMenuSceneManager.Initialize();
    }
}
