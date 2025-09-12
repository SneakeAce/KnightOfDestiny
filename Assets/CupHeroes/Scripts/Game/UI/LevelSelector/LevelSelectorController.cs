using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectorController : IDisposable
{
    private MainMenuController _mainMenuController;
    private LevelSelectorView _levelSelectorView;

    public LevelSelectorController(MainMenuController mainMenuController)
    {
        _mainMenuController = mainMenuController;
    }

    public event Action OnLevelSelected;

    public void Dispose()
    {
        _mainMenuController.OnOpenLevelSelectorWindow -= OnOpenLevelSelector;
        _levelSelectorView.OnLevelSelected -= LoadLevel;
    }

    public void SetLevelSelectorView(LevelSelectorView view) => _levelSelectorView = view;

    public void Initialize()
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
        Debug.Log($"LoadLevel. Level_{index} is Loaded");

        SceneManager.LoadScene($"Level_{index}");

        OnLevelSelected?.Invoke();
    }

}
