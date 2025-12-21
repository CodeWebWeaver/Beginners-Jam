public abstract class State : IState {
    protected readonly IStateMachine StateMachine;

    protected State(IStateMachine stateMachine) {
        StateMachine = stateMachine;
    }

    public abstract void Enter();
    public abstract void Exit();

    public virtual void Dispose() {
        // Базова реалізація (може бути порожньою)
    }
}
