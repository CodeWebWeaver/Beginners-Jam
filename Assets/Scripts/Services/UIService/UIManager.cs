using System;
using UnityEngine;
using Zenject;

public class UIManager : MonoBehaviour, IUiService {
    [SerializeField] PauseMenuUI pauseUI;
    [SerializeField] LoadingScreenUI loading;
    [Inject] IGameManager gameManager;

    public LoadingScreenUI LoadingUI => loading;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            gameManager.ExitToMainMenu();
        }
    }
    public void TogglePanel(UIPanel panel) {
        if (panel.IsOpen) {
            panel.Hide();
        } else {
            panel.Show();
        }
    }

    public void ShowPauseMenu() {
        pauseUI.Show();
    }

    public void HidePauseMenu() {
        pauseUI.Hide();
    }
}
