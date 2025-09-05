using System;
using UnityEngine;

[Serializable]
public class WaveControllerStats
{
    [field: SerializeField] public int MaxWaveOnLevel { get; private set; }
    [field: SerializeField] public int StartAmountEnemyOnWave { get; private set; }
    [field: SerializeField] public int MultiplierAmounEnemyOnWave { get; private set; }
}
