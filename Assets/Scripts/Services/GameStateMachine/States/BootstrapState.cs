using System;
using Tymski;
using UnityEngine.SceneManagement;

public class BootstrapState : State {
    private ISaveLoadService _saveLoadService;
    private SceneData _data;
    public BootstrapState(IStateMachine stateMachine, ISaveLoadService saveLoad, SceneData data) : base(stateMachine) {
        _data = data;
        _saveLoadService = saveLoad;
    }

    public override void Enter() {
        _saveLoadService.LoadAll();

        StateMachine.ChangeState<GameLoopState>();
    }

    public override void Exit() {
    }
}

[Serializable]
public class SceneData {
    public SceneReference mainMenuScene;
}