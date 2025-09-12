using System;
using UnityEngine;

[Serializable]
public class WaveControllerStats
{
    [field: SerializeField] public int MaxWaveOnLevel { get; private set; }
    [field: SerializeField] public int StartAmountEnemyOnWave { get; private set; }
    [field: SerializeField] public int MultiplierAmounEnemyOnWave { get; private set; }
    [field: SerializeField] public float DelayBeforeSpawn { get; private set; }
    [field: SerializeField, Range(0f, 1f)] public float DelayBetweenSpawnEntities { get; private set; }
}
