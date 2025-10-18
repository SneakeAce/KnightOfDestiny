using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/UIConfigs/LibraryRarityColorConfigs", fileName = "LibraryRarityColorConfigs")]
public class LibraryRarityColorConfigs : LibraryConfigsBase
{
    [field: SerializeField] public List<CardRarityPallete> RarityPalletes { get; private set; }

    public override List<T> GetConfigs<T>()
    {
        return RarityPalletes as List<T>;
    }
}
