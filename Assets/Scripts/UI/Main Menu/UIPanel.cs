using System.Collections;
using UnityEngine;
using DG.Tweening;

public class UIPanel : MonoBehaviour {
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] protected float fadeInDuration = 0.2f;
    [SerializeField] protected float fadeOutDuration = 0.3f;
    [SerializeField] protected Ease fadeInEase = Ease.OutQuad;
    [SerializeField] protected Ease fadeOutEase = Ease.InQuad;

    public bool IsOpen { get; private set; }

    private Tween _currentTween;

    protected virtual void Awake() {
        if (canvasGroup == null) {
            canvasGroup = GetComponentInChildren<CanvasGroup>();
        }

        SetImmediateState(false);
    }

    public virtual void Show() {
        _currentTween?.Kill();

        IsOpen = true;
        gameObject.SetActive(true);

        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        if (fadeInDuration <= 0f) {
            canvasGroup.alpha = 1f;
            OnShowComplete();
            return;
        }

        _currentTween = canvasGroup
            .DOFade(1f, fadeInDuration)
            .SetEase(fadeInEase)
            .SetUpdate(true)
            .OnComplete(OnShowComplete);
    }

    public virtual void Hide() {
        _currentTween?.Kill();

        IsOpen = false;

        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        if (fadeOutDuration <= 0f) {
            canvasGroup.alpha = 0f;
            gameObject.SetActive(false);
            OnHideComplete();
            return;
        }

        _currentTween = canvasGroup
            .DOFade(0f, fadeOutDuration)
            .SetEase(fadeOutEase)
            .SetUpdate(true)
            .OnComplete(() => {
                gameObject.SetActive(false);
                OnHideComplete();
            });
    }

    public void SetImmediateState(bool isVisible) {
        _currentTween?.Kill();

        IsOpen = isVisible;
        canvasGroup.alpha = isVisible ? 1f : 0f;
        canvasGroup.blocksRaycasts = isVisible;
        canvasGroup.interactable = isVisible;
        gameObject.SetActive(isVisible);

        if (isVisible) {
            OnShowComplete();
        } else {
            OnHideComplete();
        }
    }

    protected virtual void OnShowComplete() { }
    protected virtual void OnHideComplete() { }

    protected virtual void OnDestroy() {
        _currentTween?.Kill();
    }
}
