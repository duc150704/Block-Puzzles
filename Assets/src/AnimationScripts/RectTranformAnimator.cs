using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class RectTranformAnimator : MonoBehaviour
{
    private RectTransform rectTransform;
    private Tween _curTween;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public Tween Scale(Vector3 endValue, float duration, Ease ease)
    {
        _curTween = rectTransform.DOScale(endValue, duration).SetEase(ease);
        return _curTween;
    }

    public Tween ScaleLoop(Vector3 endValue, float duration, Ease ease)
    {
        _curTween = rectTransform.DOScale(endValue, duration)
            .SetEase(ease)
            .SetLoops(-1, LoopType.Yoyo);
        return _curTween;
    }

    private void OnDisable()
    {
        _curTween.Kill();
    }
}
