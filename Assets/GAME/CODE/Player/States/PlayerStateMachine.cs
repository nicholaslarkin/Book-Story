using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    PlayerState currentState;

    public PlayerState CurrentState { get; private set; }

    public void Initialize(PlayerState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerState newState)
    {
        CurrentState?.Exit();

        CurrentState = newState;

        CurrentState.Enter();
    }

    void Update()
    {
        CurrentState?.Update();
    }
}
