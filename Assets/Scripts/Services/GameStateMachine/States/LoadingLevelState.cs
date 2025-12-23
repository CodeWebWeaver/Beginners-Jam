using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LoadingLevelState : State {
    private readonly ILevelProgressService _levelProgressService;
    private readonly ISceneTransitionManager _sceneTransitionManager;
    private CancellationTokenSource _cts;

    public LoadingLevelState(
        IStateMachine stateMachine,
        ILevelProgressService levelProgressService,
        ISceneTransitionManager sceneTransitionManager)
        : base(stateMachine) {
        _levelProgressService = levelProgressService;
        _sceneTransitionManager = sceneTransitionManager;
    }

    public override async void Enter() {
        _cts = new CancellationTokenSource();
        string currentLevelName = _levelProgressService.GetCurrentLevelName();
        _sceneTransitionManager.LoadSceneWithLoadingScreenAsync(currentLevelName, _cts.Token).Forget();  
    }


    public override void Exit() {
        _cts?.Cancel();
        _cts?.Dispose();
    }
}
