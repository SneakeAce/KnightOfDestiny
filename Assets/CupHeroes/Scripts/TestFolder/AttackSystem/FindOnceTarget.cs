using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindOnceTarget : BaseTargetFinderStrategy
{
    public FindOnceTarget()
    {
    }

    public override event Action<IEnumerable<IEnemy>> OnTargetFound;

    public override void Dispose()
    {
        if (_enemies.Count > 0) 
            ResetTarget(_enemies.FirstOrDefault());   
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
                _context.SearchingRadius + OffsetSearchingRadius,
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
        _enemies.Clear();

        enemy.Health.EntityDied -= ResetTarget;
        enemy.Health.EntityDied -= _context.RestartSearching;
    }

}
