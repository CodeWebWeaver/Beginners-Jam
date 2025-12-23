using UnityEngine;

public class LoadingScreenService : ILoadingScreenService {
    public void Show() {
        Debug.Log("Loading Screen Shown");
    }
    public void Hide() {
        Debug.Log("Loading Screen Hidden");
    }
    public void UpdateProgress(float progress) {
        Debug.Log($"Loading Progress: {progress * 100}%");
    }
}