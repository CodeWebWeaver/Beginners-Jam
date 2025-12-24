using UnityEngine;

public class PauseState : State {
    private IUiService _uiService;
    public PauseState(IStateMachine stateMachine) : base(stateMachine) {
    }

    public override void Enter() {
        Time.timeScale = 0f;

        _uiService.ShowPauseMenu();
    }

    public override void Exit() {
        _uiService.HidePauseMenu();

        Time.timeScale = 1f;
    }
}
