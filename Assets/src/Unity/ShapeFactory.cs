using System.Collections.Generic;
using UnityEngine;

public class ShapeFactory
{
    public Dictionary<ShapeType, ShapeData> _data = new();

    public ShapeFactory(List<ShapeData> data)
    {
        foreach (var item in data)
        {
            _data[item.ShapeType] = item;
        }
    }

    public Shape Create(ShapeType shapeType, Direction direction = Direction.Up)
    {
        ShapeData data = _data[shapeType];
        List<Vector3> cells = new List<Vector3>(data.Cells);
        int height = data.Height;
        int width = data.Width;

        switch (direction)
        {
            case Direction.Up:
                break;
            case Direction.Right:
                cells = ShapeRotator.Rotate90CW(data.Cells, ref height, ref width, 1);
                break;
            case Direction.Down:
                cells = ShapeRotator.Rotate90CW(data.Cells, ref height, ref width, 2);
                break;            
            case Direction.Left:
                cells = ShapeRotator.Rotate90CW(data.Cells, ref height, ref width, 3);
                break;
        }

        List<GridPosition> gridCells = new List<GridPosition>();
        foreach (var item in cells)
        {
            gridCells.Add(new GridPosition(Mathf.FloorToInt(item.y), Mathf.FloorToInt(item.x)));
        }

        return new Shape(data.ShapeType, gridCells, data.CellSize, height, width, direction);
    }
}

public static class ShapeRotator
{
    public static List<Vector3> Rotate90CW(List<Vector3> positions, ref int height, ref int width, int time)
    {
        List<Vector3> result = new List<Vector3>(positions);
        while (time > 0)
        {
            int tmp = height;
            height = width;
            width = tmp;

            for (int i = 0; i < result.Count; i++)
            {
                float x = result[i].y;
                float y = height - 1 - result[i].x;

                result[i] = new Vector3(x, y, 0);
            }

            time--;
        }

        Normalize(result);
        return result;
    }

    private static void Normalize(List<Vector3> positions)
    {
        FindMinAnchor(positions, out float minX, out float minY);
        if (minX >= 0 && minY >= 0)
            return;

        Vector3 anchor = new Vector3(Mathf.Abs(minX), Mathf.Abs(minY), 0);

        for(int i = 0; i < positions.Count; i++)
        {
            positions[i] += anchor;
        }
    }

    private static void FindMinAnchor(List<Vector3> positions, out float minX, out float minY)
    {
        minX = float.MaxValue;
        minY = float.MaxValue;

        foreach (var pos in positions)
        {
            if (minX > pos.x)
                minX = pos.x;

            if (minY > pos.y)
                minY = pos.y;
        }
    }
}
