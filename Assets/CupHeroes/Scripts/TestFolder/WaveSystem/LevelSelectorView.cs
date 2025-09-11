using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorView : MonoBehaviour
{
    [SerializeField] private List<Button> _levelButtons;

    public event Action<int> OnLevelSelected;

    public void Initialize()
    {
        for (int i = 0; i < _levelButtons.Count; i++)
        {
            int levelIndex = i;
            _levelButtons[i].onClick.AddListener(() => OnLevelSelected?.Invoke(levelIndex));
        }        
    }

}
