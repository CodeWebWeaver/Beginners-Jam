using Cysharp.Threading.Tasks;
using System;
using System.Threading;

public interface ISceneLoader {
    UniTask LoadAsync(string sceneName, IProgress<float> progress = null, CancellationToken cancellationToken = default);
    void Load(string sceneName, Action onLoaded = null);
}
