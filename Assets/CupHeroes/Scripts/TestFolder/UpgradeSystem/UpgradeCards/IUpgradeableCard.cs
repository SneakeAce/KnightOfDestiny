using UnityEngine;
using UnityEngine.UI;

public interface IUpgradeableCard
{
    Transform Transform { get; }
    Transform Parent { get; }
    Button ButtonSelectCard { get; }
    UpgradeCardConfig Config { get; }
    RareCardType RareType { get; }
    UpgradeType UpgradeType { get; }
    float MultiplierValue { get; }

    void SetParent(Transform parent);
    void SetRareType(RareCardType type);
    void SetUpgradeType(UpgradeType type);
    void SetMultiplierValue(float value);
    void ReturnToPool();
    float GetUpgradeValue();
}
