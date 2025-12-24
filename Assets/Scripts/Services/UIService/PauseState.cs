using UnityEngine;

public class PauseState : State {
    private IUiService _uiService;
    public PauseState(IStateMachine stateMachine, IUiService uiService) : base(stateMachine) {
        _uiService = uiService;
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
