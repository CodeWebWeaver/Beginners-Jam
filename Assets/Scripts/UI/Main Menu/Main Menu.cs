using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuController : MonoBehaviour {
    [Inject] IUiService uiService;
    [Inject] IGameManager gameManager;
    [SerializeField] UIPanel settingsPanel;
    [SerializeField] Button startGameButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button exitButton;

    private void Start() {
        SubscribeButtons();
    }

    public void StartGame() {
        gameManager.StartNewGame();
    }

    public void ToggleSettings() {
        uiService.TogglePanel(settingsPanel);
    }

    public void ExitGame() {
        gameManager.ExitGame();
    }

    private void SubscribeButtons() {
        startGameButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(ToggleSettings);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void UnSubscribeButtons() {
        startGameButton.onClick.RemoveListener(StartGame);
        settingsButton.onClick.RemoveListener(ToggleSettings);
        exitButton.onClick.RemoveListener(ExitGame);
    }

    private void OnDestroy() {
        UnSubscribeButtons();
    }
}