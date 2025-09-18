using UnityEngine;

public interface IUpgrade
{
    UpgradeType Type { get; }

    void ModifyValue(ref float value);
    
}
