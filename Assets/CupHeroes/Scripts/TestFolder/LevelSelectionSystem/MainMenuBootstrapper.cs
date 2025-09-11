using System;

public class MainMenuBootstrapper : ILevelBootstrapper
{
    private MainMenuView _mainMenuView;
    private MainMenuController _mainMenuController;

    private LevelSelectorView _levelSelectorView;
    private LevelSelectorController _levelSelectorController;

    public MainMenuBootstrapper(MainMenuView mainMenuView, MainMenuController mainMenuController,
        LevelSelectorView levelSelectorView, LevelSelectorController levelSelectorController)
    {
        _mainMenuView = mainMenuView;
        _mainMenuController = mainMenuController;
        _levelSelectorView = levelSelectorView;
        _levelSelectorController = levelSelectorController;

        Initialize();
    }

    public event Action OnInitialized;

    public void Initialize()
    {
        _mainMenuView.Initialize();
        _mainMenuController.Initialize();

        _levelSelectorView.Initialize();
        _levelSelectorController.Initialize();
    }
}
