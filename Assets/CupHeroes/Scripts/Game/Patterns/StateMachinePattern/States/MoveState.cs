using UnityEngine;

public class MoveState : IEntityState
{
    private IEntity _entity;
    private Vector2 _pointToMove;

    private float _moveSpeed;
    private bool _isMoving;

    public MoveState(IEntity entity, Vector2 pointToMove)
    {
        _entity = entity;
        _pointToMove = pointToMove;
    }

    public void Enter()
    {
        _moveSpeed = _entity.Config.MoveStats.MoveSpeed;

        _isMoving = true;

        float animationMultiplierSpeed = _entity.Animator.speed + _moveSpeed;
        _entity.Animator.SetFloat("WalkMultiplierSpeed", animationMultiplierSpeed);
        _entity.Animator.SetBool("IsMoving", true);
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
        _isMoving = false;
        _entity.Animator.SetBool("IsMoving", _isMoving);
    }

}
