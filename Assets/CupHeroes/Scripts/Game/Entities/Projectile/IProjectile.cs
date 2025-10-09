using UnityEngine;

public interface IProjectile
{
    Transform Transform { get; }
    Collider2D Collider { get; }
    Rigidbody2D Rigidbody { get; }
    Animator Animator { get; }
    ProjectileConfig ProjectileConfig { get; }
    ProjectileMoveController MoveComponent { get; }
    ProjectileController Controller { get; }
    IEntity Parent { get; }

    void Initialize();
    void SetParent(IEntity parent);
    void SetController(ProjectileController controller);
    void SetPool(IObjectPool currentPool);
    void SetConfig(ProjectileConfig config);
}
