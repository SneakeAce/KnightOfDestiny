using System;
using UnityEngine;

public class LevelController : IDisposable
{
    private IEntity _character;
    private ICharacterController _characterController;

    private EnemyWaveController _waveController;

    private Vector2 _nextPosition = new Vector2(10f, 0f);

    public event Action OnStartWave;
    public event Action OnEndLevel;

    public void Construct(Character character, EnemyWaveController waveController)
    {
        _character = character;
        _waveController = waveController;
    }

    public void Initialize()
    {
        _characterController = _character.EntityController as ICharacterController;

        _waveController.Initialize();

        SubscribingEvents();

        StartWave();
    }

    public void StartWave()
    {
        Debug.Log("StartWave");
        OnStartWave?.Invoke();
    }

    public void EndLevel()
    {
        _waveController.StopWave();
    }

    public void Dispose()
    {
        UnsubscribingEvents();
    }

    private void SubscribingEvents()
    {
        OnStartWave += _waveController.StartWave;
        
        _waveController.IsWaveDone += WavePassed;
    }

    private void UnsubscribingEvents()
    {
        OnStartWave -= _waveController.StartWave;

        _characterController.IsCharacterOnPosition -= StartWave;

        _waveController.IsWaveDone -= WavePassed;
    }

    private void WavePassed()
    {
        _characterController.IsCharacterOnPosition += StartWave;

        _characterController.SetPositionToMove(_nextPosition);
        _characterController.SetMoveCommand();

        _waveController.SetOffset(_nextPosition);
    }

}
