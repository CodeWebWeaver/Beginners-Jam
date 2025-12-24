using System;
using UnityEngine;
using Zenject;

public class UIManager : MonoBehaviour, IUiService {
    [SerializeField] PauseMenuUI pauseUI;
    [SerializeField] LoadingScreenUI loading;
    public LoadingScreenUI LoadingUI => loading;

    public PauseMenuUI PauseUI => pauseUI;

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
