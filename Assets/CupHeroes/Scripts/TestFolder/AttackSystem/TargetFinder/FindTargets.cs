using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTargets : IDisposable
{
    protected const float MinTimeBeforeSearchTarget = 0.1f;
    protected const float MaxTimeBeforeSearchTarget = 0.6f;
    protected const float OffsetSearchingRadius = 1.5f;

    private TargetFinderController _controller;

    private List<IEntity> _targets = new();

    private IEntity _closestTarget;
    private IEntity _previousTarget;

    private IEntity _character;
    private Transform _characterTransofrm;
    private float _searchingRadius;
    private int _amountAvailableTargets;
    private LayerMask _targetsLayer;

    public FindTargets(TargetFinderController controller)
    {
        _controller = controller;
    }

    public event Action<IEnumerable<IEntity>> TargetsFounded;
    public event Action<IEntity> ClosestTargetFounded;

    public void Initialize()
    {
        SetData();
    }

    public void Dispose()
    {
        if (_targets.Count > 0)
        {
            for (int i = 0; i < _targets.Count; i++)
            {
                var tar = _targets[i];

                if (tar == null)
                    continue;

                ResetTarget(tar);
            }
        }

        if (_closestTarget != null)
        {
            _closestTarget.Health.EntityDied -= ResetTarget;
        }
    }

    public IEnumerator SearchTargetsJob()
    {
        while (_character != null)
        {
            Debug.Log($"FindTarget - SearchTargetsJob - _searchingRadius = {_searchingRadius}");
            CheckValidateData();

            yield return new WaitForSeconds(
                UnityEngine.Random.Range(MinTimeBeforeSearchTarget,
                MaxTimeBeforeSearchTarget)
                );

            Collider2D[] targets = Physics2D.OverlapCircleAll(
                _characterTransofrm.position,
                _searchingRadius + OffsetSearchingRadius,
                _targetsLayer
                );

            for (int i = 0; i < targets.Length && _targets.Count < _amountAvailableTargets; i++)
            {
                if (targets[i].TryGetComponent<IEnemy>(out var enemy))
                {
                    enemy.Health.EntityDied += ResetTarget;

                    if (_targets.Contains(enemy) == false)
                        _targets.Add(enemy);

                    TargetsFounded?.Invoke(_targets);
                }
            }

            FindClosestTarget();
        }
    }

    private void SetData()
    {
        if (_character == null)
        {
            _character = _controller.Character;
            _characterTransofrm = _character.Transform;
        }

        _searchingRadius = _controller.SearchingRadius;

        _amountAvailableTargets = _controller.AmountAvailableTargets;

        _targetsLayer = _controller.TargetsLayer;
    }

    private void CheckValidateData()
    {
        _searchingRadius = _controller.SearchingRadius;

        _amountAvailableTargets = _controller.AmountAvailableTargets;
    }

    private void FindClosestTarget()
    {
        if (_character == null)
            return;

        float minSqr = float.MaxValue;

        for (int i = 0; i < _targets.Count; i++)
        {
            var target = _targets[i];

            if (target == null || target == _character)
                return;

            var sqrDistanceToTarget = (target.Transform.position - _characterTransofrm.position).sqrMagnitude;

            if (sqrDistanceToTarget < minSqr)
            {
                minSqr = sqrDistanceToTarget;
                _closestTarget = target;
            }

            if (_closestTarget != null && _previousTarget != _closestTarget)
            {
                Debug.Log($"{this.ToString()} - FindClosestTarget - {_previousTarget} != {_closestTarget}");

                ClosestTargetFounded?.Invoke(_closestTarget);

                _previousTarget = _closestTarget;
            }
        }
    }

    private void ResetTarget(IEntity enemy)
    {
        _targets.Remove(enemy);

        enemy.Health.EntityDied -= ResetTarget;
    }

}
