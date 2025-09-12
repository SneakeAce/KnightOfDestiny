using UnityEngine;

public class MainMenuSceneManager
{
    private IConfigsProvider _configsProvider;
    private IMainMenuUIObjectFactory _mainMenuUIObjectFactory;

    private MainMenuSceneConfig _sceneConfig;

    private MainMenuController _mainMenuController;
    private LevelSelectorController _levelSelectorController;

    public MainMenuSceneManager(IConfigsProvider configsProvider, IMainMenuUIObjectFactory mainMenuUIObjectFactory,
        MainMenuController mainMenuController, LevelSelectorController levelSelectorController)
    {
        _configsProvider = configsProvider;
        _mainMenuUIObjectFactory = mainMenuUIObjectFactory;

        _mainMenuController = mainMenuController;
        _levelSelectorController = levelSelectorController;
    }

    public MainMenuView MainMenuView { get; private set; }
    public LevelSelectorView LevelSelectorView { get; private set; }

    public void Initialize()
    {
        _sceneConfig = _configsProvider.GetSingleConfig<MainMenuSceneConfig>().GetConfig<MainMenuSceneConfig>();

        CreateMainMenuObject();

        CreateLevelSelectorObject();
    }

    private void CreateMainMenuObject()
    {
        GameObject prefab = _sceneConfig.MainMenuPrefab;
        MainMenuView = _mainMenuUIObjectFactory.CreateObject<MainMenuView>(prefab);

        MainMenuView.Initialize();
        MainMenuView.gameObject.SetActive(true);

        _mainMenuController.SetMainMenuView(MainMenuView);
        _mainMenuController.Initialize();
    }

    private void CreateLevelSelectorObject()
    {
        GameObject prefab = _sceneConfig.LevelSelectorPrefab;
        LevelSelectorView = _mainMenuUIObjectFactory.CreateObject<LevelSelectorView>(prefab);

        LevelSelectorView.Initialize();
        LevelSelectorView.gameObject.SetActive(false);

        _levelSelectorController.SetLevelSelectorView(LevelSelectorView);
        _levelSelectorController.Initialize();
    }
}
