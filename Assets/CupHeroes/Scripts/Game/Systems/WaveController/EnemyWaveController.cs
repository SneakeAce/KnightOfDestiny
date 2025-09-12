using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveController : IDisposable
{
    private EnemySpawner _spawner;
    private WaveControllerConfig _config;

    private CoroutinePerformer _coroutinePerformer;

    private Coroutine _startWaveCoroutine;
    private Coroutine _startTrackingWaveCoroutine;

    private List<IEntity> _subsribedEnemies = new();

    private int _maxWaves;
    private int _maxEnemyOnWave;
    private int _amountEnemyMultiplier;

    private int _currentAmountEnemy;
    private int _amountEnemyOnWave;
    private int _currentWave;

    private float _delayBeforeSpawn;
    private float _delayBetweenSpawnEntities;

    private Quaternion _startRotation;
    private float _startPositionByX;
    private float _startPositionByY;

    private float _minStartPositionOffsetByX;
    private float _maxStartPositionOffsetByX;
    private float _minStartPositionOffsetByY;
    private float _maxStartPositionOffsetByY;

    private Vector2 _offsetSpawnPositionInNextWave;

    public EnemyWaveController(EnemySpawner spawner, IConfigsProvider configsProvider,
        CoroutinePerformer coroutinePerformer)
    {
        _spawner = spawner;

        _config = configsProvider.GetSingleConfig<WaveControllerConfig>().GetConfig<WaveControllerConfig>();

        _coroutinePerformer = coroutinePerformer;
    }

    public event Action IsWaveDone;

    public void Initialize()
    {
        _subsribedEnemies.Clear();

        SetParameters();
    }

    public void Dispose()
    {
        for (int i = 0; i < _subsribedEnemies.Count; i++)
        {
            IEntity entity = _subsribedEnemies[i];

            if (entity != null)
                entity.Health.EntityDied -= DecreaseCurrentAmountEnemy;
        }

        _subsribedEnemies.Clear();
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

        StartCoroutine(ref _startWaveCoroutine, StartWaveJob());
    }

    public void StopWave()
    {
        StartCoroutine(ref _startWaveCoroutine, StartWaveJob(), true);
        StartCoroutine(ref _startTrackingWaveCoroutine, StartTrackingWaveJob(), true);
    }

    public void SetOffset(Vector2 offset)
    {
        _offsetSpawnPositionInNextWave += offset;
    }

    private void WaveDone()
    {
        Debug.Log("WaveController. WaveDone");

        _maxEnemyOnWave = _maxEnemyOnWave + _amountEnemyMultiplier;
        _amountEnemyOnWave = 0;

        StartCoroutine(ref _startWaveCoroutine, StartWaveJob(), true);
        StartCoroutine(ref _startTrackingWaveCoroutine, StartTrackingWaveJob(), true);
    }

    private void SetParameters()
    {
        _maxEnemyOnWave = _config.WaveControllerStats.StartAmountEnemyOnWave;
        _maxWaves = _config.WaveControllerStats.MaxWaveOnLevel;
        _amountEnemyMultiplier = _config.WaveControllerStats.MultiplierAmounEnemyOnWave;
        _delayBeforeSpawn = _config.WaveControllerStats.DelayBeforeSpawn;
        _delayBetweenSpawnEntities = _config.WaveControllerStats.DelayBetweenSpawnEntities;

        _startPositionByX = _config.StartPositionStats.StartPositionByX;
        _startPositionByY = _config.StartPositionStats.StartPositionByY;

        _minStartPositionOffsetByX = _config.StartPositionStats.MinStartPositionOffsetByX;
        _maxStartPositionOffsetByX = _config.StartPositionStats.MaxStartPositionOffsetByX;
        _minStartPositionOffsetByY = _config.StartPositionStats.MinStartPositionOffsetByY;
        _maxStartPositionOffsetByY = _config.StartPositionStats.MaxStartPositionOffsetByY;

        _startRotation = _config.StartPositionStats.StartPositionRotation;

        _currentWave = 0;
        _amountEnemyOnWave = 0;
    }

    private IEnumerator StartWaveJob()
    {
        yield return new WaitForSeconds(_delayBeforeSpawn);

        while (_amountEnemyOnWave < _maxEnemyOnWave)
        {
            Vector2 startPosition = GetStartPosition();

            IEnemy enemy = _spawner.SpawnEnemy(startPosition, _startRotation);

            if (enemy == null)
            {
                yield return null;
                continue;
            }

            enemy.Health.EntityDied += DecreaseCurrentAmountEnemy;

            _amountEnemyOnWave++;
            _currentAmountEnemy++;

            _subsribedEnemies.Add(enemy);

            yield return new WaitForSeconds(_delayBetweenSpawnEntities);
        }

        StartCoroutine(ref _startTrackingWaveCoroutine, StartTrackingWaveJob());
    }

    private IEnumerator StartTrackingWaveJob()
    {
        while (_currentAmountEnemy > 0)
        {
            yield return null;
        }

        WaveDone();
        IsWaveDone?.Invoke();
    }

    private Vector2 GetStartPosition()
    {
        Vector2 startPosition = Vector2.zero;

        float startPositionByX = _startPositionByX + UnityEngine.Random.Range(
            _minStartPositionOffsetByX, 
            _maxStartPositionOffsetByX
            ) + _offsetSpawnPositionInNextWave.x;

        float startPositionByY = _startPositionByY + UnityEngine.Random.Range(
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

    private void DecreaseCurrentAmountEnemy(IEntity entity)
    {
        _currentAmountEnemy--;

        _subsribedEnemies.Remove(entity);

        entity.Health.EntityDied -= DecreaseCurrentAmountEnemy;
    }

    private Coroutine StartCoroutine(ref Coroutine routine, IEnumerator enumerator, bool onlyStopRoutine = false)
    {
        if (routine != null)
        {
            _coroutinePerformer.StopCoroutine(routine);
            routine = null;
        }

        if (onlyStopRoutine)
            return null;

        routine = _coroutinePerformer.StartCoroutine(enumerator);

        return routine;
    }

}
