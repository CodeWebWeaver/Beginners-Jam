public class LoadingScreenService : ILoadingScreenService {
    private readonly IUiService _uiService;

    public LoadingScreenService(IUiService uiService) {
        _uiService = uiService;
    }

    public void ShowLoading() => _uiService.LoadingUI.Show();
    public void HideLoading() => _uiService.LoadingUI.Hide();
    public void UpdateLoadingProgress(float progress) => _uiService.LoadingUI.UpdateProgress(progress);
}
