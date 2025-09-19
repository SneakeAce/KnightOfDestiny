using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTargetFinderStrategy : ITargetFinderStrategy, IDisposable
{
    protected const float MinTimeBeforeSearchTarget = 0.1f;
    protected const float MaxTimeBeforeSearchTarget = 0.6f;
    protected const float OffsetSearchingRadius = 12f;

    protected TargetFinderContext _context;

    protected List<IEnemy> _enemies;

    public BaseTargetFinderStrategy()
    {
    }

    public abstract event Action<IEnumerable<IEnemy>> OnTargetsFound;
    public abstract event Action OnTargetDissapeared;

    public abstract IEnumerator SearchTargetsJob();
    public abstract void ResetTarget(IEntity enemy);
    public abstract void Dispose();

    public void Initialize(TargetFinderContext context)
    {
        _context = context;

        _enemies = new List<IEnemy>(_context.AmountAvailableTargets);

        _enemies.Clear();
    }

}
