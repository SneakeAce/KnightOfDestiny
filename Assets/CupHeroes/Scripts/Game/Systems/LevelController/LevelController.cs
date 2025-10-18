using System;
using UnityEngine;

public class LevelController : IDisposable
{
    private IEntity _character;
    private ICharacterController _characterController;

    private EnemyWaveController _waveController;

    private Vector2 _nextPosition = new Vector2(10f, 0f);

    public event Action StartWave;
    public event Action WaveDone;
    public event Action EndLevel;

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

        OnStartWave();
    }

    public void OnStartWave()
    {
        Debug.Log("StartWave");
        StartWave?.Invoke();
    }

    public void OnEndLevel()
    {
        _waveController.StopWave();
    }

    public void Dispose()
    {
        UnsubscribingEvents();
    }

    private void SubscribingEvents()
    {
        StartWave += _waveController.StartWave;
        
        _waveController.IsWaveDone += WavePassed;
    }

    private void UnsubscribingEvents()
    {
        StartWave -= _waveController.StartWave;

        _characterController.IsCharacterOnPosition -= OnStartWave;

        _waveController.IsWaveDone -= WavePassed;
    }

    private void WavePassed()
    {
        _characterController.IsCharacterOnPosition += OnStartWave;

        _characterController.SetPositionToMove(_nextPosition);
        _characterController.SetMoveCommand();

        _waveController.SetOffset(_nextPosition);

        WaveDone?.Invoke();
    }

}
