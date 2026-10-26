using UnityEngine;


public class ShapeShadow : MonoBehaviour
{
    public static readonly string Layer = "Preview";

    public static ShapeShadow Create(ShapeView shapeView, Sprite sprite)
    {
        GameObject newGo = new GameObject("Shadow");
        //newGo.AddComponent<SpriteRenderer>().sortingLayerName = Layer;
        foreach (var pos in shapeView.Shape.Cells)
        {
            GameObject cell = new GameObject("ShadowCell");
            SpriteRenderer sr = cell.AddComponent<SpriteRenderer>();
            sr.sortingLayerName = Layer;
            sr.sprite = sprite;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.4f);

            cell.transform.localPosition =
                new Vector3(pos.Column, pos.Row) * shapeView.Shape.CellSize;

            cell.transform.SetParent(newGo.transform);
        }

        newGo.SetActive(false);
        return newGo.AddComponent<ShapeShadow>();
    }
    
    public void Show(Vector3 position)
    {
        if (gameObject.activeSelf == false)
            gameObject.SetActive(true);

        transform.position = position;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
