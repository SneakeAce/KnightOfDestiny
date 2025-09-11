using System;
using UnityEngine.SceneManagement;

public class LevelSelectorController : IDisposable
{
    private MainMenuController _mainMenuController;
    private LevelSelectorView _levelSelectorView;

    public LevelSelectorController(MainMenuController mainMenuController, LevelSelectorView levelSelectorView)
    {
        _mainMenuController = mainMenuController;
        _levelSelectorView = levelSelectorView;

        Initialize();
    }

    public event Action OnLevelSelected;

    public void Dispose()
    {
        _mainMenuController.OnOpenLevelSelectorWindow -= OnOpenLevelSelector;
        _levelSelectorView.OnLevelSelected -= LoadLevel;
    }

    private void Initialize()
    {
        _mainMenuController.OnOpenLevelSelectorWindow += OnOpenLevelSelector;
        _levelSelectorView.OnLevelSelected += LoadLevel;
    }


    private void OnOpenLevelSelector()
    {
        _levelSelectorView.gameObject.SetActive(true);
    }

    private void LoadLevel(int index)
    {
        SceneManager.LoadScene($"Level_{index}");

        OnLevelSelected?.Invoke();
    }

}
