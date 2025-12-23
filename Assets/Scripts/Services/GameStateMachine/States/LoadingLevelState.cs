using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LoadingLevelState : State {
    private readonly ILevelProgressService _levelProgressService;
    private readonly ISceneLoader _sceneLoader;
    private readonly ILoadingScreenService _loadingScreenService;

    private CancellationTokenSource _cts;

    public LoadingLevelState(
        IStateMachine stateMachine,
        ILevelProgressService levelProgressService,
        ISceneLoader sceneLoader,
        ILoadingScreenService loadingScreenService)
        : base(stateMachine) {
        _levelProgressService = levelProgressService;
        _sceneLoader = sceneLoader;
        _loadingScreenService = loadingScreenService;
    }

    public override async void Enter() {
        _cts = new CancellationTokenSource();
        LoadLevelAsync(_cts.Token).Forget();
    }

    private async UniTaskVoid LoadLevelAsync(CancellationToken ct) {
        try {
            string sceneName = _levelProgressService.GetCurrentLevelName();

            _loadingScreenService.Show();

            var progress = Progress.Create<float>(x => _loadingScreenService.UpdateProgress(x));

            using (var timeoutController = new TimeoutController()) {
                var linkedToken = CancellationTokenSource.CreateLinkedTokenSource(ct, timeoutController.Timeout(300)).Token;

                await _sceneLoader.LoadAsync(sceneName, progress, linkedToken);
            }

            // 4. Переходимо в гру
            StateMachine.ChangeState<GameLoopState>();
        } catch (OperationCanceledException) {
            Debug.LogWarning("Loading was cancelled or timed out.");
            HandleLoadingError("Timeout/Cancellation");
        } catch (Exception ex) {
            Debug.LogError($"Loading error: {ex.Message}");
            HandleLoadingError(ex.Message);
        } finally {
            _loadingScreenService.Hide();
        }
    }

    public override void Exit() {
        _cts?.Cancel();
        _cts?.Dispose();
    }

    private void HandleLoadingError(string reason) {
        // StateMachine.ChangeState<MainMenuState>();
    }
}
