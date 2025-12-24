public interface IUiService {
    LoadingScreenUI LoadingUI { get; }

    void TogglePanel(UIPanel panel);
    void ShowPauseMenu();
    void HidePauseMenu();
}
