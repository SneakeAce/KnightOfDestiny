using System;
using System.Collections;
using UnityEngine;

public class TargetFinder
{
    private const float MinTimeBeforeSearchTarget = 0.2f;
    private const float MaxTimeBeforeSearchTarget = 1.2f;
    private const float OffsetSearchingRadius = 2f;

    private IEntity _character;
    private IEnemy _currentTarget;

    private CoroutinePerformer _performer;
    private Coroutine _searchTargetCoroutine;

    private float _searchingRadius;

    private LayerMask _targetsLayer;

    public TargetFinder(IEntity character, CoroutinePerformer performer)
    {
        _character = character;
        _performer = performer;
    }

    public event Action<IEnemy> OnTargetFound;

    public void Initialize()
    {
        SetParameters();

        RestartCoroutine(ref _searchTargetCoroutine, SearchTargetJob());
    }

    private void SetParameters()
    {
        _searchingRadius = _character.Config.AttackStats.AttackRange + OffsetSearchingRadius;
        _targetsLayer = _character.Config.AttackStats.TargetLayer;
    }

    private IEnumerator SearchTargetJob()
    {
        while (_currentTarget == null && _character != null)
        {
            yield return new WaitForSeconds(
                UnityEngine.Random.Range(MinTimeBeforeSearchTarget, 
                MaxTimeBeforeSearchTarget)
                );

            Collider2D target = Physics2D.OverlapCircle(
                _character.Transform.position, 
                _searchingRadius, 
                _targetsLayer
                );

            if (target != null && target.TryGetComponent<IEnemy>(out IEnemy enemy))
            {
                _currentTarget = enemy;

                _currentTarget.Health.EntityDied += ResetTarget;

                OnTargetFound?.Invoke(_currentTarget);
            }
        }
    }

    private void ResetTarget(IEntity enemy)
    {
        _currentTarget.Health.EntityDied -= ResetTarget;

        _currentTarget = null;

        RestartCoroutine(ref _searchTargetCoroutine, SearchTargetJob());
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
}
