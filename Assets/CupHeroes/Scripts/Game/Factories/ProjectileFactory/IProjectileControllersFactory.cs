using System.Collections.Generic;

public interface IProjectileControllersFactory
{
    List<ProjectileController> ProjectileControllers { get; }
    ProjectileController CreateProjectileController(IProjectile projectile, IEntity target);

}
