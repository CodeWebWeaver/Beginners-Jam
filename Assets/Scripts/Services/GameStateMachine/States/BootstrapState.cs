using System;
using Tymski;
using UnityEngine.SceneManagement;

public class BootstrapState : State {
    private ISaveLoadService _saveLoadService;
    private SceneData _sceneData;
    public BootstrapState(IStateMachine stateMachine, ISaveLoadService saveLoad, SceneData data) : base(stateMachine) {
        _sceneData = data;
        _saveLoadService = saveLoad;
    }

    public override void Enter() {
        _saveLoadService.LoadAll();


        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == _sceneData.mainMenuScene.SceneName) {
            StateMachine.ChangeState<MainMenuState>();
            return;
        }

        StateMachine.ChangeState<GameLoopState>();
    }

    public override void Exit() {
    }
}

[Serializable]
public class SceneData {
    public SceneReference mainMenuScene;
}