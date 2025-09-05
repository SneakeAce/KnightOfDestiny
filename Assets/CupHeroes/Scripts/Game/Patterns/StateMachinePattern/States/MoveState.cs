using UnityEngine;

public class MoveState : IEntityState
{
    private IEntity _entity;
    private Vector2 _pointToMove;

    public MoveState(IEntity entity, Vector2 pointToMove)
    {
        _entity = entity;
        _pointToMove = pointToMove;
    }

    public void Enter()
    {
        _entity?.Animator.SetTrigger("Move");
    }

    public void Update()
    {
        _entity?.Rigidbody.MovePosition(Vector2.MoveTowards(
            _entity.Rigidbody.position,
            _pointToMove,
            _entity.Config.MoveStats.MoveSpeed * Time.fixedDeltaTime));
    }

    public void Exit()
    {
        _entity?.Animator.SetTrigger("Move");
    }

}
