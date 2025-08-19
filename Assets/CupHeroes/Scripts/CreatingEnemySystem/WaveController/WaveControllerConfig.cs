using UnityEngine;

[CreateAssetMenu(menuName = "Configs/WaveControllerConfig", fileName = "WaveControllerConfig")]
public class WaveControllerConfig : ScriptableObject
{
    [field: SerializeField] public int MaxWaveOnLevel { get; private set; }
    [field: SerializeField] public int StartAmountEnemyOnWave { get; private set; }
    [field: SerializeField] public int MultiplierAmounEnemyOnWave { get; private set; }
}
