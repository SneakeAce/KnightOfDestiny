using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Button _startGameButton;

    public event Action OnStartGameClicked;

    public void Initialize()
    {
        _canvas.worldCamera = Camera.main;

        _startGameButton.onClick.AddListener(() => OnStartGameClicked?.Invoke());
    }
}
