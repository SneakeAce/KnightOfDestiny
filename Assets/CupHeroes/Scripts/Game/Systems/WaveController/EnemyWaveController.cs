using System.Collections;
using UnityEngine;

public class EnemyWaveController
{
    private const float OffsetSpawnByAxes = 1.5f;

    private EnemySpawner _spawner;
    private WaveControllerConfig _config;

    private CoroutinePerformer _coroutinePerformer;

    private Coroutine _startWaveCoroutine;

    private int _maxWaves;
    private int _maxEnemyOnWave;
    private int _amountEnemyMultiplier;

    private int _currentCountEnemyOnWave;
    private int _currentWave;

    public EnemyWaveController(EnemySpawner spawner, IConfigsProvider configsProvider,
        CoroutinePerformer coroutinePerformer)
    {
        _spawner = spawner;

        _config = configsProvider.GetSingleConfig<WaveControllerConfig>().GetConfig<WaveControllerConfig>();

        _coroutinePerformer = coroutinePerformer;
    }

    public void Initialize()
    {
        SetParameters();
    }

    public void StartWave()
    {
        _currentWave++;

        if (_currentWave == _maxWaves)
        {
            Debug.Log("LastWave!");
        }
        else if (_currentWave >= _maxWaves)
        {
            Debug.Log("Level end!");
            return;
        }

        _currentCountEnemyOnWave = 0;

        if (_startWaveCoroutine != null)
        {
            _coroutinePerformer.StopCoroutine(_startWaveCoroutine);
            _startWaveCoroutine = null;
        }

        _startWaveCoroutine = _coroutinePerformer.StartCoroutine(StartWaveJob());
    }

    private void WaveDone()
    {
        _maxEnemyOnWave = _maxEnemyOnWave + _amountEnemyMultiplier;

        if (_startWaveCoroutine != null)
        {
            _coroutinePerformer.StopCoroutine(_startWaveCoroutine);
            _startWaveCoroutine = null;
        }

        StartWave();
    }

    private void SetParameters()
    {
        _maxEnemyOnWave = _config.StartAmountEnemyOnWave;
        _maxWaves = _config.MaxWaveOnLevel;
        _amountEnemyMultiplier = _config.MultiplierAmounEnemyOnWave;

        _currentWave = 0; 
    }

    private IEnumerator StartWaveJob()
    {
        while (_currentCountEnemyOnWave < _maxEnemyOnWave)
        {
            var tempStartPosition = new Vector2(100f, 0f);

            var tempStartRotation = Quaternion.Euler(0f, 180f, 0f);

            IEnemy enemy = _spawner.SpawnEnemy(tempStartPosition, tempStartRotation);

            if (enemy == null)
            {
                Debug.LogError("Enemy in WaveController is null!");
                yield return null;
                continue;
            }

            _currentCountEnemyOnWave++;

            yield return null;
        }

        yield return new WaitForSeconds(10f);

        WaveDone();
    }

}
