public class IdleState : IEntityState
{
    private IEntity _entity;

    private bool _isEnableIdleState;

    public IdleState(IEntity entity)
    {
        _entity = entity;
    }

    public void Enter()
    {
        _isEnableIdleState = true;
        _entity?.Animator.SetBool("IsIdle", _isEnableIdleState);
    }

    public void Exit()
    {
        _isEnableIdleState = false;
        _entity?.Animator.SetBool("IsIdle", _isEnableIdleState);
    }

    public void Update()
    {
        return;
    }
}
