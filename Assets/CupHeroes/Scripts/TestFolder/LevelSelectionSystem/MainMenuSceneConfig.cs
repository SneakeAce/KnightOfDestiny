using UnityEngine;

[CreateAssetMenu(menuName = "Configs/SceneConfigs/MainMenuSceneConfig", fileName = "MainMenuSceneConfig")]
public class MainMenuSceneConfig : SingleConfigBase
{
    [field: SerializeField] public GameObject MainMenuPrefab { get; private set; }
    [field: SerializeField] public GameObject LevelSelectorPrefab { get; private set; }

    public override T GetConfig<T>()
    {
        return this as T;
    }
}
