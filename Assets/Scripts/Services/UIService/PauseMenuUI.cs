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
        gameManager.ResumeGame();
    }

    public override void Show() {
        Time.timeScale = 0f;
        base.Show();
    }
    public override void Hide() {
        Time.timeScale = 1f;
        base.Hide();
    }
}
