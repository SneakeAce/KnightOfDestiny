using System;

public class MainMenuController : IDisposable
{
    private MainMenuView _mainMenuView;

    public event Action OnOpenLevelSelectorWindow;

    public void Dispose()
    {
        _mainMenuView.OnStartGameClicked -= OpenLevelSelector;
    }

    public void SetMainMenuView(MainMenuView mainMenuView)
    {
        _mainMenuView = mainMenuView;
    }

    public void Initialize()
    {
        _mainMenuView.OnStartGameClicked += OpenLevelSelector;
    }

    private void OpenLevelSelector()
    {
        _mainMenuView.gameObject.SetActive(false);

        OnOpenLevelSelectorWindow?.Invoke();
    }
}
