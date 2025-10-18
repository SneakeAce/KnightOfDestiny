using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/UIConfigs/LibraryUpgradeColorConfigs", fileName = "LibraryUpgradeColorConfigs")]
public class LibraryUpgradeColorConfigs : LibraryConfigsBase
{
    [field: SerializeField] public List<UpgradeTypePallete> UpgradeTypePallets { get; private set; }

    public override List<T> GetConfigs<T>()
    {
        return UpgradeTypePallets as List<T>;
    }
}
