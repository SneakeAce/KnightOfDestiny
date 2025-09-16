using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMultipleTargets : BaseTargetFinderStrategy
{
    public FindMultipleTargets()
    {
    }

    public override event Action<IEnumerable<IEnemy>> OnTargetFound;

    public override void Dispose()
    {
        throw new NotImplementedException();
    }

    public override IEnumerator SearchTargetsJob()
    {
        while (_context.CanSearching && _context.Character != null)
        {
            _context.UpdateData();

            yield return new WaitForSeconds(
                UnityEngine.Random.Range(MinTimeBeforeSearchTarget,
                MaxTimeBeforeSearchTarget)
                );

            Collider2D target = Physics2D.OverlapCircle(
                _context.Character.Transform.position,
                _context.SearchingRadius,
                _context.TargetsLayer
                );

            if (target != null && target.TryGetComponent<IEnemy>(out IEnemy enemy))
            {
                enemy.Health.EntityDied += ResetTarget;
                enemy.Health.EntityDied += _context.RestartSearching;

                _enemies.Add(enemy);

                OnTargetFound?.Invoke(_enemies);

                yield break;
            }
        }
    }

    public override void ResetTarget(IEntity enemy)
    {
        throw new NotImplementedException();
    }

}
