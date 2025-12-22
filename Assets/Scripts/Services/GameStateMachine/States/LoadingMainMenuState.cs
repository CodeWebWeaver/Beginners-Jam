using UnityEngine;

public class LoadingMainMenuState : State {
    private readonly ISceneLoader _sceneLoader;
    private readonly ILoadingScreenService _loadingScreenService;
    public LoadingMainMenuState(IStateMachine stateMachine, ISceneLoader sceneLoader) : base(stateMachine) { }
    public override void Enter() {
        Debug.Log("Entered Main Menu State");
        _sceneLoader.Load("MainMenu", () => {
            StateMachine.ChangeState<MainMenuState>();
        });
    }
    public override void Exit() {
        Debug.Log("Exited Main Menu State");
    }
}
