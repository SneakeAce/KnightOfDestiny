using System;
using UnityEngine;

public class LevelManager : IDisposable
{
    private IEntity _character;
    private ICharacterController _characterController;

    private EnemyWaveController _waveController;

    private Vector2 _nextPosition = new Vector2(10f, 0f);

    public event Action OnStartLevel;
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
    }

    public void StartLevel()
    {
        OnStartLevel?.Invoke();
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
        OnStartLevel += _waveController.StartWave;

        _waveController.IsWaveDone += WavePassed;
    }

    private void UnsubscribingEvents()
    {
        OnStartLevel -= _waveController.StartWave;

        _waveController.IsWaveDone -= WavePassed;
    }

    private void WavePassed()
    {
        _characterController.SetPositionToMove(_nextPosition);
        _character.EntityController.SetMoveCommand();
    }

}
