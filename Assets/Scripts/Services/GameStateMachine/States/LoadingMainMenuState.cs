using Cysharp.Threading.Tasks;
using UnityEngine;

public class LoadingMainMenuState : State {
    private readonly ISceneLoader _sceneLoader;
    private readonly ILoadingScreenService _loadingScreenService;
    private readonly SceneData _sceneData;
    public LoadingMainMenuState(IStateMachine stateMachine, ISceneLoader sceneLoader, SceneData sceneData) : base(stateMachine) { 
        _sceneLoader = sceneLoader;
        _sceneData = sceneData;
    }
    public override void Enter() {

        LoadScene().Forget();
    }

    private async UniTaskVoid LoadScene() {
        await _sceneLoader.LoadAsync(_sceneData.mainMenuScene.SceneName);
             StateMachine.ChangeState<MainMenuState>();
    }

    public override void Exit() {
    }
}
