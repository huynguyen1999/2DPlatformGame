using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    public State CurrentState { get; private set; }
    public State PreviousState { get; private set; }

    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(State newState, object data = null)
    {
        if (newState == CurrentState)
            return;
        CurrentState.Exit();
        PreviousState = CurrentState;
        CurrentState = newState;
        CurrentState.Enter(data);
    }
}
