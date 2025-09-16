using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTargetFinderStrategy : ITargetFinderStrategy, IDisposable
{
    protected const float MinTimeBeforeSearchTarget = 0.2f;
    protected const float MaxTimeBeforeSearchTarget = 1.2f;
    protected const float OffsetSearchingRadius = 2f;

    protected TargetFinderContext _context;

    protected List<IEnemy> _enemies = new List<IEnemy>();

    public BaseTargetFinderStrategy()
    {
    }

    public abstract event Action<IEnumerable<IEnemy>> OnTargetFound;

    public abstract IEnumerator SearchTargetsJob();
    public abstract void ResetTarget(IEntity enemy);
    public abstract void Dispose();

    public void Initialize(TargetFinderContext context)
    {
        _context = context;
    }

}
