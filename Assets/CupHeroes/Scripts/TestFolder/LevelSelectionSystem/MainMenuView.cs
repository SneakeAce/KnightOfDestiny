using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;

    public event Action OnStartGameClicked;

    public void Initialize()
    {
        _startGameButton.onClick.AddListener(() => OnStartGameClicked?.Invoke());
    }
}
