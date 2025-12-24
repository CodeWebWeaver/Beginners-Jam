public interface IUiService {
    LoadingScreenUI LoadingUI { get; }
    PauseMenuUI PauseUI { get; }
    void TogglePanel(UIPanel panel);
    void ShowPauseMenu();
    void HidePauseMenu();
}
