using UnityEngine;

public class MainMenuState : State {
    public MainMenuState(IStateMachine stateMachine) : base(stateMachine) { }
    public override void Enter() {
        Debug.Log("In Main Menu");
    }
    public override void Exit() {
        Debug.Log("Leaving Main Menu");
    }
}