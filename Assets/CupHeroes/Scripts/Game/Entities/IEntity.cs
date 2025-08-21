using UnityEngine;

public interface IEntity
{
    Transform Transform { get; }
    Collider2D Collider { get; }
    Rigidbody2D Rigidbody { get; }
    Animator Animator { get; }
    EntityConfig Config { get; }
    IEntityStateMachine StateMachine { get; }
    IEntityHealth Health { get; }

    void SetConfig(EntityConfig config);

}
