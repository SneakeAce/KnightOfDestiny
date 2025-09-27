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

    public override event Action<IEnumerable<IEnemy>> OnTargetsFound;
    public override event Action OnTargetDissapeared;

    public override void Dispose()
    {
        if (_enemies.Count > 0) 
            ResetTarget(_enemies.FirstOrDefault());   
    }

    public override IEnumerator SearchTargetsJob()
    {
        while (_enemies.Count < 0 && _context.Character != null)
        {
            _context.UpdateData();

            yield return new WaitForSeconds(
                UnityEngine.Random.Range(MinTimeBeforeSearchTarget,
                MaxTimeBeforeSearchTarget)
                );

            Collider2D targetCol = Physics2D.OverlapCircle(
                _context.Character.Transform.position,
                _context.SearchingRadius + OffsetSearchingRadius,
                _context.TargetsLayer
                );

            if (targetCol != null && targetCol.TryGetComponent<IEnemy>(out var target))
            {
                target.Health.EntityDied += ResetTarget;

                _enemies.Add(target);

                OnTargetsFound?.Invoke(_enemies);

                yield break;
            }
        }
    }

    public override void ResetTarget(IEntity target)
    {
        _enemies.Clear();

        target.Health.EntityDied -= ResetTarget;

        OnTargetDissapeared?.Invoke();
    }

}
