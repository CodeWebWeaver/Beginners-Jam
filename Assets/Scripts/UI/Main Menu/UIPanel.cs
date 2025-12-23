using System.Collections;
using UnityEngine;
public class UIPanel : MonoBehaviour {
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] protected float fadeInDuration = 0f;
    [SerializeField] protected float fadeOutDuration = 0.3f;

    public bool IsOpen { get; private set; }

    private Coroutine currentFadeCoroutine;

    protected virtual void Awake() {
        if (canvasGroup == null) {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        SetPanelState(false, 0f);
    }

    public virtual void Show() {
        if (currentFadeCoroutine != null) {
            StopCoroutine(currentFadeCoroutine);
        }

        IsOpen = true;
        currentFadeCoroutine = StartCoroutine(FadeIn());
    }

    public virtual void Hide() {
        if (currentFadeCoroutine != null) {
            StopCoroutine(currentFadeCoroutine);
        }

        IsOpen = false;
        currentFadeCoroutine = StartCoroutine(FadeOut());
    }

    protected virtual IEnumerator FadeIn() {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        if (fadeInDuration <= 0f) {
            canvasGroup.alpha = 1f;
            currentFadeCoroutine = null;
            yield break;
        }

        float startAlpha = canvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeInDuration) {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, elapsedTime / fadeInDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
        currentFadeCoroutine = null;
    }

    protected virtual IEnumerator FadeOut() {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        if (fadeOutDuration <= 0f) {
            canvasGroup.alpha = 0f;
            currentFadeCoroutine = null;
            yield break;
        }

        float startAlpha = canvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeOutDuration) {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeOutDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        currentFadeCoroutine = null;
    }

    private void SetPanelState(bool isVisible, float alpha) {
        canvasGroup.alpha = alpha;
        canvasGroup.blocksRaycasts = isVisible;
        canvasGroup.interactable = isVisible;
        IsOpen = isVisible;
    }

    protected virtual void OnDestroy() {
        if (currentFadeCoroutine != null) {
            StopCoroutine(currentFadeCoroutine);
        }
    }
}
