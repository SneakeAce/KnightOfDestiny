using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/UpgradeCards", fileName = "UpdgradeCardConfig")]
public class UpgradeCardConfig : ScriptableObject
{
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public UpgradeType UpgradeType { get; private set; }
    [field: SerializeField] public float UpgradeValue { get; private set; }
    [field: SerializeField] public List<MultipliersValueCardStats> MultipliersUpgradeValueFromRarity { get; private set; }
}
