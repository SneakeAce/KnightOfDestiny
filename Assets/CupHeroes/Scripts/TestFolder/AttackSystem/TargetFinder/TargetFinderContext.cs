using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinderContext : IDisposable
{
    private ITargetFinderStrategy _strategy;
    private ICharacter _character;

    private CoroutinePerformer _performer;
    private Coroutine _searchTargetCoroutine;

    private LayerMask _targetsLayer;

    private int _amountAvailableTargets;
    private float _searchingRadius;

    public TargetFinderContext(ICharacter character, ITargetFinderStrategy strategy, CoroutinePerformer performer)
    {
        _character = character;
        _strategy = strategy;
        _performer = performer;
    }

    public ICharacter Character { get => _character; }
    public CoroutinePerformer Performer { get => _performer; }
    public LayerMask TargetsLayer { get => _targetsLayer; }
    public int AmountAvailableTargets { get => _amountAvailableTargets; }
    public float SearchingRadius { get => _searchingRadius; }

    public event Action<IEnumerable<IEnemy>> OnTargetsFound;

    public void Dispose()
    {
        _character.Health.EntityDied -= OnCharacterDead;

        _strategy.OnTargetsFound -= OnTargetFound;
        _strategy.OnTargetDissapeared -= RestartSearching;
    }

    public void Initialize()
    {
        _character.Health.EntityDied += OnCharacterDead;

        _strategy.Initialize(this);

        _strategy.OnTargetsFound += OnTargetFound;
        _strategy.OnTargetDissapeared += RestartSearching;

        UpdateData();

        RestartCoroutine(ref _searchTargetCoroutine, _strategy.SearchTargetsJob());
    }

    public void RestartSearching()
    {
        RestartCoroutine(ref _searchTargetCoroutine, _strategy.SearchTargetsJob());
    }

    public void UpdateData()
    {
        _searchingRadius = _character.StatsManager.AttackStats.RangeAttackRange;
        _amountAvailableTargets = _character.StatsManager.AttackStats.AmountTargetsForAttack;
        _targetsLayer = _character.Config.AttackStats.TargetLayer;
    }

    private void OnTargetFound(IEnumerable<IEnemy> enemies)
    {
        OnTargetsFound?.Invoke(enemies);
    }

    protected Coroutine RestartCoroutine(ref Coroutine routine, IEnumerator enumerator)
    {
        if (routine != null)
        {
            _performer.StopCoroutine(routine);
            routine = null;
        }

        routine = _performer.StartCoroutine(enumerator);

        return routine;
    }

    private void OnCharacterDead(IEntity enitity)
    {
        _character.Health.EntityDied -= OnCharacterDead;

        _character = null;
    }

}
