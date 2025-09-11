using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorView : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private List<Button> _levelButtons;

    public event Action<int> OnLevelSelected;

    public void Initialize()
    {
        _canvas.worldCamera = Camera.main;

        for (int i = 0; i < _levelButtons.Count; i++)
        {
            int levelIndex = i + 1;
            _levelButtons[i].onClick.AddListener(() => OnLevelSelected?.Invoke(levelIndex));
        }        
    }

}
