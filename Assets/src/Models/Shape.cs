using System.Collections.Generic;
using UnityEngine;

public enum ShapeType
{
    Square3x3,
    Square2x2,
    TShape,
    LShape,
    Zigzac,
    Line3,
    Line4,
    Line5,
    Line2,
    SquareConer,
    Square2x3
}

[System.Serializable]
public class Shape
{

    [SerializeField] private ShapeType _shapeType;
    [SerializeField] private List<GridPosition> _cells;
    [SerializeField] private float _cellSize;
    [SerializeField] private int _height;
    [SerializeField] private int _width;
    [SerializeField] private Direction _direction;
    
    public ShapeType ShapeType => _shapeType;
    public List<GridPosition> Cells => _cells;  
    public float CellSize => _cellSize;
    public int Height => _height;
    public int Width => _width;
    public Direction Direction => _direction;

    public Shape(ShapeType shapeType, List<GridPosition> cells, float cellSize, int height, int width, Direction direction)
    {
        _shapeType = shapeType;
        _cells = cells;
        _cellSize = cellSize;
        _height = height;
        _width = width;
        _direction = direction;  
    }
}
