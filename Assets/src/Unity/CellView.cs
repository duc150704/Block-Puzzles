using DG.Tweening;
using UnityEngine;

public class CellView : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;

    private SpriteRenderer _spriteRenderer;
    private TranformAnimator _animator;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<TranformAnimator>();
        SetSprite(_sprite);
    }

    public Tween Scale(Vector3 scale, float duration, Ease ease)
    {
        return _animator.Scale(scale, duration, ease);
    }

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    public Sprite GetSprite()
    {
        return _spriteRenderer.sprite;
    }

    public void SetSpriteLayer(string layerName, int orderInLayer = 0)
    {
        _spriteRenderer.sortingLayerName = layerName;
        _spriteRenderer.sortingOrder = orderInLayer;
    }
}
