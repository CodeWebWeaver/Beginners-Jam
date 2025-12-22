public class BootstrapState : State {
    private ISaveLoadService _saveLoadService;
    private IAudioService _audioService;
    private ILevelProgressService _levelProgress;
    public BootstrapState(IStateMachine stateMachine, ISaveLoadService saveLoadService, IAudioService audioService, ILevelProgressService levelProgressService) : base(stateMachine) {
        _saveLoadService = saveLoadService;
        _audioService = audioService;
        _levelProgress = levelProgressService;
    }

    public override void Enter() {
        _saveLoadService.LoadAll();
        StateMachine.ChangeState<GameLoopState>();
    }

    public override void Exit() {
    }
}
