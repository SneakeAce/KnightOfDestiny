using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinderController : IDisposable
{
    private ICharacter _character;

    private FindTargets _findTargets;

    private CoroutinePerformer _performer;
    private Coroutine _searchTargetCoroutine;

    private LayerMask _targetsLayer;

    private int _amountAvailableTargets;
    private float _searchingRadius;

    public TargetFinderController(ICharacter character, CoroutinePerformer performer)
    {
        _character = character;
        _performer = performer;
    }

    public ICharacter Character { get => _character; }
    public LayerMask TargetsLayer { get => _targetsLayer; }
    public int AmountAvailableTargets { get => _amountAvailableTargets; }
    public float SearchingRadius { get => _searchingRadius; }

    public event Action<IEnumerable<IEntity>> TargetsFounded;
    public event Action<IEntity> ClosestTargetFounded;

    public void Dispose()
    {
        _character.Health.EntityDied -= OnCharacterDead;

        _findTargets.TargetsFounded -= OnTargetsFound;
        _findTargets.ClosestTargetFounded -= OnClosestTargetFound;
    }

    public void Initialize()
    {
        UpdateData();

        InitializeFindTargets();

        SubscribingEvents();

        RestartCoroutine(ref _searchTargetCoroutine, _findTargets.SearchTargetsJob());
    }

    public void UpdateData()
    {
        _searchingRadius = _character.StatsManager.AttackStats.RangeAttackRange;
        _amountAvailableTargets = _character.StatsManager.AttackStats.AmountTargetsForAttack;
        _targetsLayer = _character.Config.AttackStats.TargetLayer;
    }

    private void InitializeFindTargets()
    {
        _findTargets = new FindTargets(this);

        _findTargets.Initialize();
    }

    private void SubscribingEvents()
    {
        _character.Health.EntityDied += OnCharacterDead;

        _findTargets.TargetsFounded += OnTargetsFound;
        _findTargets.ClosestTargetFounded += OnClosestTargetFound;
    }

    private void OnTargetsFound(IEnumerable<IEntity> enemies)
    {
        TargetsFounded?.Invoke(enemies);
    }

    private void OnClosestTargetFound(IEntity closestTarget)
    {
        ClosestTargetFounded?.Invoke(closestTarget);
    }

    private Coroutine RestartCoroutine(ref Coroutine routine, IEnumerator enumerator)
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
