using Cysharp.Threading.Tasks;
using System.Threading;

public interface ISceneTransitionManager {
    UniTask LoadSceneWithLoadingScreenAsync(string sceneName, CancellationToken cancellationToken = default);
}
