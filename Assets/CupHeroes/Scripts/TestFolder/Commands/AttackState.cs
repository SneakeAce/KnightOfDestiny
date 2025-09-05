using UnityEngine;

public class AttackState : IEntityState
{
    public void Enter()
    {
        Debug.Log("Enter AttackState");
    }

    public void Exit()
    {
        Debug.Log("Exit AttackState");
    }

    public void Update()
    {
        return;
    }
}
