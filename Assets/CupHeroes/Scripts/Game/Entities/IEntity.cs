using UnityEngine;

public interface IEntity
{
    Transform Transform { get; }
    Collider2D Collider { get; }
    Rigidbody2D Rigidbody { get; }
    Animator Animator { get; }
    EntityConfig Config { get; }
    IAnimationEventReceiver AnimationEventReceiver { get; }
    IEntityStateMachine StateMachine { get; }
    IEntityHealth Health { get; }
    IEntityController EntityController { get; }

    void Initialize();
    void SetConfig(EntityConfig config);
    void SetController(IEntityController controller);
}
