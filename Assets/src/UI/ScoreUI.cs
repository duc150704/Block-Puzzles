using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private RectTranformAnimator _animator;
    private Vector3 _originalScale;

    private void Awake()
    {
        _animator = GetComponent<RectTranformAnimator>();
        _originalScale = transform.localScale;
    }

    private void OnEnable()
    {
        GameManager.Instance.Score.OnScoreChanged += HandleOnScoreChanged;
    }

    private void OnDisable()
    {
        GameManager.Instance.Score.OnScoreChanged -= HandleOnScoreChanged;
    }

    private void HandleOnScoreChanged(int score)
    {
        _scoreText.text = score.ToString();
        Sequence sequence = DOTween.Sequence();

        sequence.Append( _animator.Scale(Vector3.one * 1.2f, 0.1f, DG.Tweening.Ease.InBounce));
        sequence.Append( _animator.Scale(_originalScale, 0.1f, DG.Tweening.Ease.InBounce));
    }
}
