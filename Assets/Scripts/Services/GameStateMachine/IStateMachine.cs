using System;
using UnityEngine;

public interface IStateMachine {
    IState CurrentState { get; }

    void ChangeState<T>() where T : class, IState;
}

public class StateMachine : IStateMachine {
    private IState _currentState;
    private IStateFactory stateFactory;

    public IState CurrentState => _currentState;

    public StateMachine(IStateFactory stateFactory) {
        this.stateFactory = stateFactory;
    }

    public void ChangeState<T>() where T : class, IState {
        if (_currentState is T) {
            Debug.Log($"Already in state: {typeof(T).Name}");
            return;
        }

        IState newState = stateFactory.CreateState<T>();
        _currentState?.Exit();

        //Debug.Log("Exiting Bootstrap State");

        (_currentState as IDisposable)?.Dispose();

        _currentState = newState;
        _currentState.Enter();

        //Debug.Log("Entering Bootstrap State");
    }
}
