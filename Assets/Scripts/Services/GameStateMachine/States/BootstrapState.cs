public class BootstrapState : State {
    private ISaveLoadService saveLoadService;
    private IAudioService audioService;
    public BootstrapState(IStateMachine stateMachine, ISaveLoadService saveLoadService, IAudioService audioService) : base(stateMachine) {
        this.saveLoadService = saveLoadService;
        this.audioService = audioService;
    }

    public override void Enter() {
        AudioServiceSaveAdapter audioServiceSaveAdapter = new AudioServiceSaveAdapter(audioService);
        saveLoadService.Register<AudioSettings>(audioServiceSaveAdapter);

        saveLoadService.LoadAll();
        StateMachine.ChangeState<GameLoopState>();
    }

    public override void Exit() {
    }
}
