using System;
using UnityEngine;

public interface IStateMachine {
    void ChangeState<T>() where T : class, IState;
}

public class StateMachine : IStateMachine {
    private IState _currentState;
    private IStateFactory stateFactory;

    public StateMachine(IStateFactory stateFactory) {
        this.stateFactory = stateFactory;
    }

    public void ChangeState<T>() where T : class, IState {
        IState newState = stateFactory.CreateState<T>();
        _currentState?.Exit();

        //Debug.Log("Exiting Bootstrap State");

        (_currentState as IDisposable)?.Dispose();

        _currentState = newState;
        _currentState.Enter();

        //Debug.Log("Entering Bootstrap State");
    }
}
