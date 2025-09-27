using System;
using UnityEngine;
using Zenject;

public class ProjectileController : ITickable, IDisposable
{
    private IEntity _target;
    private IEntity _parent;

    private IProjectile _projectile;

    private CoroutinePerformer _performer;

    public event Action<IProjectile> ProjectileDestroyed;

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public void Tick()
    {
        throw new NotImplementedException();
    }



}
