using System;

public class MainMenuController : IDisposable
{
    private MainMenuView _mainMenuView;

    public MainMenuController(MainMenuView mainMenuView)
    {
        _mainMenuView = mainMenuView;

        Initialize();
    }

    public event Action OnOpenLevelSelectorWindow;

    public void Dispose()
    {
        _mainMenuView.OnStartGameClicked -= OpenLevelSelector;
    }

    private void Initialize()
    {
        _mainMenuView.OnStartGameClicked += OpenLevelSelector;
    }

    private void OpenLevelSelector()
    {
        _mainMenuView.gameObject.SetActive(false);

        OnOpenLevelSelectorWindow?.Invoke();
    }
}
