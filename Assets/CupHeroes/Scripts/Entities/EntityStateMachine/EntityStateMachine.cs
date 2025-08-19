using UnityEngine;

public class EntityStateMachine : MonoBehaviour, IEntityStateMachine
{
    private IEntityState _currentState;

    public void SetState(IEntityState state)
    {
        _currentState?.Exit();

        _currentState = state;

        _currentState?.Enter();
    }

    private void Update()
    {
        _currentState.Update();
    }
}
