using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinderContext
{
    private ITargetFinderStrategy _strategy;
    private ICharacter _character;

    private CoroutinePerformer _performer;
    private Coroutine _searchTargetCoroutine;

    private LayerMask _targetsLayer;

    private int _amountAvailableTargets;
    private float _searchingRadius;
    private bool _canSearching;

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
    public bool CanSearching { get => _canSearching; }

    public event Action<IEnumerable<IEnemy>> OnTargetsFound;

    public void Initialize()
    {
        _strategy.Initialize(this);
        _strategy.OnTargetFound += OnTargetFound;

        UpdateData();

        RestartCoroutine(ref _searchTargetCoroutine, _strategy.SearchTargetsJob());
    }

    public void RestartSearching(IEntity enitity)
    {
        RestartCoroutine(ref _searchTargetCoroutine, _strategy.SearchTargetsJob());
    }

    public void UpdateData()
    {
        _searchingRadius = _character.StatsManager.AttackStats.AttackRange;
        _amountAvailableTargets = _character.StatsManager.AttackStats.AmountTargetsForAttack;
        _targetsLayer = _character.Config.AttackStats.TargetLayer;
    }

    private void OnTargetFound(IEnumerable<IEnemy> enemies)
    {
        OnTargetsFound?.Invoke(enemies);

        _canSearching = false;
    }

    protected Coroutine RestartCoroutine(ref Coroutine routine, IEnumerator enumerator)
    {
        _canSearching = !_canSearching;

        if (routine != null)
        {
            _performer.StopCoroutine(routine);
            routine = null;
        }

        routine = _performer.StartCoroutine(enumerator);

        return routine;
    }
}
