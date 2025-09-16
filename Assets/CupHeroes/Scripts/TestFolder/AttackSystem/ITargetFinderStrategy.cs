using System.Collections.Generic;
using System;
using System.Collections;

public interface ITargetFinderStrategy
{
    event Action<IEnumerable<IEnemy>> OnTargetFound;

    void Initialize(TargetFinderContext context);
    IEnumerator SearchTargetsJob();
}
