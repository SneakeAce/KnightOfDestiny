using UnityEngine;

public class MoveCommand : ICommand
{
    private IEntity _entity;
    private Vector2 _pointToMove;

    public MoveCommand(IEntity entity, Vector2 pointToMove)
    {
        _entity = entity;
        _pointToMove = pointToMove;
    }

    public void Execute()
    {
        _entity.StateMachine.SetState(new MoveState(_entity, _pointToMove));
    }

    public void CancelCommand()
    {
        _entity.StateMachine.RemoveState();
    }
}
