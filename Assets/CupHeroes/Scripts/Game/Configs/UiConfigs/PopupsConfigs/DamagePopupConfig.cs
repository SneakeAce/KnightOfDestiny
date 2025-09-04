using UnityEngine;

[CreateAssetMenu(menuName = "Configs/UIConfigs/DamagePopupConfig", fileName = "DamagePopupConfig")]
public class DamagePopupConfig : UIConfig
{

    [field: SerializeField] public UIElementType Type { get; private set; }

    public override int GetTypeId() => (int)Type;
}
