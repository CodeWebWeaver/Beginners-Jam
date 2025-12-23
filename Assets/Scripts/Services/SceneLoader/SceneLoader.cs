using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine.SceneManagement;

public class SceneLoader : ISceneLoader {
    public async UniTask LoadAsync(string sceneName, IProgress<float> progress = null, CancellationToken cancellationToken = default) {
        if (string.IsNullOrEmpty(sceneName))
            throw new ArgumentException("Scene name is empty", nameof(sceneName));

        await SceneManager.LoadSceneAsync(sceneName)
            .ToUniTask(progress: progress, cancellationToken: cancellationToken);
    }

    public void Load(string sceneName, Action onLoaded = null) {
        LoadAndForget(sceneName, onLoaded).Forget();
    }

    private async UniTaskVoid LoadAndForget(string sceneName, Action onLoaded) {
        await LoadAsync(sceneName);
        onLoaded?.Invoke();
    }
}
