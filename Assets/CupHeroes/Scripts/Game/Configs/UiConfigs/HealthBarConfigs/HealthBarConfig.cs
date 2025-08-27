using UnityEngine;

[CreateAssetMenu(menuName = "Configs/UIConfigs/HealthBarConfig", fileName = "HealthBarConfig")]
public class HealthBarConfig : UIConfig
{
    [field: SerializeField] public UIElementType Type { get; private set; }

    public override int GetTypeId() => (int)Type;
}
