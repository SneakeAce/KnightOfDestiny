using System.Collections;
using UnityEngine;

public class EnemyWaveController
{
    private EnemySpawner _spawner;
    private WaveControllerConfig _config;

    private CoroutinePerformer _coroutinePerformer;

    private Coroutine _startWaveCoroutine;

    private int _maxWaves;
    private int _maxEnemyOnWave;
    private int _amountEnemyMultiplier;

    private int _currentCountEnemyOnWave;
    private int _currentWave;

    private Quaternion _startRotation;
    private float _startPositionByX;
    private float _startPositionByY;

    private float _minStartPositionOffsetByX;
    private float _maxStartPositionOffsetByX;
    private float _minStartPositionOffsetByY;
    private float _maxStartPositionOffsetByY;


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

        //_currentCountEnemyOnWave = 0;

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
        _maxEnemyOnWave = _config.WaveControllerStats.StartAmountEnemyOnWave;
        _maxWaves = _config.WaveControllerStats.MaxWaveOnLevel;
        _amountEnemyMultiplier = _config.WaveControllerStats.MultiplierAmounEnemyOnWave;

        _startPositionByX = _config.StartPositionStats.StartPositionByX;
        _startPositionByY = _config.StartPositionStats.StartPositionByY;

        _minStartPositionOffsetByX = _config.StartPositionStats.MinStartPositionOffsetByX;
        _maxStartPositionOffsetByX = _config.StartPositionStats.MaxStartPositionOffsetByX;
        _minStartPositionOffsetByY = _config.StartPositionStats.MinStartPositionOffsetByY;
        _maxStartPositionOffsetByY = _config.StartPositionStats.MaxStartPositionOffsetByY;

        _startRotation = _config.StartPositionStats.StartPositionRotation;

        _currentWave = 0;
        _currentCountEnemyOnWave = 0;
    }

    private IEnumerator StartWaveJob()
    {
        while (_currentCountEnemyOnWave < _maxEnemyOnWave)
        {
            Vector2 startPosition = GetStartPosition();

            IEnemy enemy = _spawner.SpawnEnemy(startPosition, _startRotation);

            if (enemy == null)
            {
                yield return null;
                continue;
            }

            _currentCountEnemyOnWave++;

            yield return null;
        }

        yield return new WaitForSeconds(10f);

        WaveDone();
    }

    private Vector2 GetStartPosition()
    {
        Vector2 startPosition = Vector2.zero;

        float startPositionByX = _startPositionByX + Random.Range(
            _minStartPositionOffsetByX, 
            _maxStartPositionOffsetByX
            );

        float startPositionByY = _startPositionByY + Random.Range(
            _minStartPositionOffsetByY, 
            _maxStartPositionOffsetByY
            );

        startPosition = new Vector2(startPositionByX, startPositionByY);

        if (startPosition == Vector2.zero)
        {
            Debug.Log("EnemyWaveController. Enemy StartPosition is Vector2.zero!");
            return Vector2.zero;
        }

        return startPosition;
    }
}
