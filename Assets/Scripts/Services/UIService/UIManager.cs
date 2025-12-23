using UnityEngine;
using Zenject;

public class UIManager : MonoBehaviour, IUiService, ILoadingScreenService {
    [SerializeField] LoadingScreenUI loadingScreenUI;

    [Inject] IGameManager gameManager;
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            gameManager.OpenMenu();
        }
    }
    public void TogglePanel(UIPanel panel) {
        if (panel.IsOpen) {
            panel.Hide();
        } else {
            panel.Show();
        }
    }

    public void HideLoading() {
        loadingScreenUI.Hide();
    }

    public void ShowLoading() {
        loadingScreenUI.Show();
    }

    public void UpdateLoadingProgress(float progress) {
        loadingScreenUI.UpdateProgress(progress);
    }
}