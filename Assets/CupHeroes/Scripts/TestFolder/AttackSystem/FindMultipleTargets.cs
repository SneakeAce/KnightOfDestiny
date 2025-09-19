using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMultipleTargets : BaseTargetFinderStrategy
{
    public FindMultipleTargets()
    {
    }

    public override event Action<IEnumerable<IEnemy>> OnTargetsFound;
    public override event Action OnTargetDissapeared;

    public override void Dispose()
    {
        if (_enemies.Count > 0)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                var tar = _enemies[i];

                if (tar == null)
                    continue;

                ResetTarget(tar);
            }
        }
    }

    public override IEnumerator SearchTargetsJob()
    {
        while (_context.Character != null)
        {
            _context.UpdateData();

            yield return new WaitForSeconds(
                UnityEngine.Random.Range(MinTimeBeforeSearchTarget,
                MaxTimeBeforeSearchTarget)
                );

            Collider2D[] targets = Physics2D.OverlapCircleAll(
                _context.Character.Transform.position,
                _context.SearchingRadius + OffsetSearchingRadius,
                _context.TargetsLayer
                );

            for (int i = 0; i < targets.Length && _enemies.Count < _context.AmountAvailableTargets; i++)
            {
                if (targets[i].TryGetComponent<IEnemy>(out var enemy))
                {
                    enemy.Health.EntityDied += ResetTarget;

                    if (_enemies.Contains(enemy) == false)
                        _enemies.Add(enemy);

                    OnTargetsFound?.Invoke(_enemies);
                }
            }
        }
    }

    public override void ResetTarget(IEntity enemy)
    {
        _enemies.Remove((IEnemy)enemy);

        enemy.Health.EntityDied -= ResetTarget;
    }

}
