using Cysharp.Threading.Tasks;
using System;
using System.Threading;

public class SceneTransitionManager : ISceneTransitionManager {
    private readonly ISceneLoader _sceneLoader;
    private readonly ILoadingScreenService _loadingScreenService;
    public SceneTransitionManager(ISceneLoader sceneLoader, ILoadingScreenService loadingScreenService) {
        _sceneLoader = sceneLoader;
        _loadingScreenService = loadingScreenService;
    }
    public async UniTask LoadSceneWithLoadingScreenAsync(string sceneName, CancellationToken cancellationToken = default) {
        _loadingScreenService.ShowLoading();
        var progress = new Progress<float>(p => {
            _loadingScreenService.UpdateLoadingProgress(p);
        });
        await _sceneLoader.LoadAsync(sceneName, progress, cancellationToken);
        _loadingScreenService.HideLoading();
    }
}