public class ExitState : State {
    private ISaveLoadService saveLoadService;
    public ExitState(IStateMachine stateMachine, ISaveLoadService saveLoadService) : base(stateMachine) {
        this.saveLoadService = saveLoadService;
    }

    public override void Enter() {
        saveLoadService.SaveAll();
    }

    public override void Exit() {
    }
}
