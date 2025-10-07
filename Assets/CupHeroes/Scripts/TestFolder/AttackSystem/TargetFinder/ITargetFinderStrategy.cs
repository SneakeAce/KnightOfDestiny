using System.Collections.Generic;
using System;
using System.Collections;

public interface ITargetFinderStrategy
{
    event Action<IEnumerable<IEnemy>> OnTargetsFound;
    event Action OnTargetDissapeared;

    void Initialize(TargetFinderController context);
    IEnumerator SearchTargetsJob();
}
