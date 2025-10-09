using System;
using UnityEngine;

public struct ProjectileSpawnData
{
    public ProjectileSpawnData(Vector2 spawnPosition, Quaternion spawnRotation, 
        IEntity parent, IEntity target,
        ProjectileType type)
    {
        SpawnPosition = spawnPosition;
        SpawnRotation = spawnRotation;
        Parent = parent;
        Target = target;
        Type = type;
    }

    public Vector2 SpawnPosition { get; }
    public Quaternion SpawnRotation { get; }
    public IEntity Parent { get; }
    public IEntity Target { get; }
    public ProjectileType Type { get; }
}

public class ProjectileSpawner
{
    private IProjectileFactory _projectileFactory;

    private IProjectileControllersFactory _projectileControllersFactory;

    public ProjectileSpawner(IProjectileFactory projectileFactory,
        IProjectileControllersFactory projectileControllersFactory)
    {
        _projectileFactory = projectileFactory;
        _projectileControllersFactory = projectileControllersFactory;
    }

    public IProjectile SpawnProjectile(ProjectileSpawnData data)
    {
        IProjectile projectile = _projectileFactory.CreateObject(data.Type);

        if (projectile == null)
            throw new ArgumentNullException($"{nameof(projectile)} in {this.ToString()} is null!");

        projectile.SetParent(data.Parent);

        projectile.Transform.position = data.SpawnPosition;
        projectile.Transform.rotation = data.SpawnRotation;

        CreateProjectileController(projectile, data);

        return projectile;
    }

    private void CreateProjectileController(IProjectile projectile, ProjectileSpawnData data) 
    {
        ProjectileController controller = _projectileControllersFactory.CreateProjectileController(projectile, data.Target);

        if (controller == null)
            throw new ArgumentNullException($"{nameof(controller)} in {this.ToString()} is null!");

        projectile.SetController(controller);
    }


}
