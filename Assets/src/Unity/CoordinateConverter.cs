using UnityEngine;

public class CoordinateConverter
{
    private float _cellSize;
    private Vector3 _translationVector;

    public CoordinateConverter(float cellSize, Vector3 translationVector)
    {
        _cellSize = cellSize;
        _translationVector = translationVector;
    }

    public Vector3 GridToWorld(int row, int columns)
    {
        float x = columns * _cellSize;
        float y = row * _cellSize;

        return new Vector3(x + _cellSize / 2, y + _cellSize / 2) + _translationVector;
    }

    public GridPosition WorldToGrid(Vector3 position)
    {
        position -= _translationVector;

        int row = Mathf.FloorToInt(position.y / _cellSize);
        int column = Mathf.FloorToInt(position.x / _cellSize);

        return new GridPosition(row, column);
    }
}
