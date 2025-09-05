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

    public void RemoveState()
    {
        _currentState?.Exit();

        _currentState = null;
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        _currentState.Update();
    }
}
