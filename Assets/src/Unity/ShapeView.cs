using System.Collections.Generic;
using UnityEngine;

public class ShapeView : MonoBehaviour
{
    [SerializeField] private GameObject _cellPrefab;

    private List<CellView> _cells = new();
    private Shape _shape;
    private SpriteRenderer _spriteRenderer;
    private ShapeShadow _shadow;

    public Shape Shape => _shape;
    public Vector3 OriginalPosition { get; set; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnDestroy()
    {
        if (_shadow != null)
            Destroy(_shadow.gameObject);
    }

    public void Create(Shape shape)
    {
        _shape = shape;

        foreach (var item in shape.Cells)
        {
            CellView cell = Instantiate(_cellPrefab, transform).GetComponent<CellView>();

            cell.transform.localPosition =
                new Vector3(item.Column, item.Row) * shape.CellSize;

            cell.SetSpriteLayer(_spriteRenderer.sortingLayerName);
            _cells.Add(cell);
        }

        _shadow = ShapeShadow.Create(this, _cells[0].GetSprite());
    }

    public void ShowPreview(Vector3 pos)
        => _shadow.Show(pos);

    public void HidePreview()
        => _shadow.Hide();

    public void ReScale(float scale)
    {
        transform.localScale = Vector3.one * scale;
    }

    public void SetPosition(Vector3 pos)
    {
        Vector3 offset = CalculateOffset();
        transform.position = pos + offset;
    }

    public void SetSprite(Sprite sprite)
    {
        foreach (var item in _cells)
        {
            SpriteRenderer sr = item.GetComponent<SpriteRenderer>();

            if (sr != null)
                sr.sprite = sprite;
        }
    }

    public static ShapeView Create(GameObject pref)
    {
        GameObject shapeObj = Instantiate(pref);
        return shapeObj.GetComponent<ShapeView>();
    }

    private Vector3 CalculateOffset()
    {
        return new Vector3(
            -(_shape.Width - 1) * _shape.CellSize/ 2f * transform.localScale.x,
            -(_shape.Height - 1) * _shape.CellSize / 2f * transform.localScale.y,
            0f
        );
    }

}
