using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenUI : UIPanel {
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private TextMeshProUGUI statusText;

    public void UpdateProgress(float progress) {
        loadingSlider.value = progress;
        if (progressText != null) {
            progressText.text = $"{progress * 100:F1}%";
        }

        if (progress >= 0.9f) {
            ToggleStatusText(false);
        }
    }

    private void ToggleStatusText(bool isEnabled) {
        if (statusText == null) return;

        statusText.enabled = isEnabled;
    }

    public override void Show() {
        base.Show();

        ToggleStatusText(true);
        loadingSlider.value = 0;
    }
}

