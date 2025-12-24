using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PauseMenuUI : UIPanel {
    [SerializeField] private Button continueBurron;
    [Inject] IGameManager gameManager;

    protected override void Awake() {
        base.Awake();
        continueBurron.onClick.AddListener(OnContinueButtonClicked);
    }

    private void OnContinueButtonClicked() {
        gameManager.TogglePause(); // here is error sometimes ui needs just close not pause
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        continueBurron.onClick.RemoveListener(OnContinueButtonClicked);
    }
}
