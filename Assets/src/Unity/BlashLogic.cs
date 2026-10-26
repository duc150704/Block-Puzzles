using System.Collections.Generic;

public class BlashLogic
{
    private Board _board;

    public BlashLogic(Board board)
    {
        _board = board;
    }

    public bool HasEnoughSpaceFor(HashSet<GridPosition> gridPositions)
    {
        foreach (var item in _board.Positions())
        {
            HashSet<GridPosition> occupyPositions = new();

            foreach (GridPosition pos in gridPositions)
                occupyPositions.Add(pos + item);

            if (_board.CanPlace(occupyPositions))
                return true;
        }
        return false;
    }

    public ScanResult Scan(GridPosition original, Shape placedShape)
    {
        HashSet<GridPosition> clearPositions = new();
        int clearedRows = 0;
        int clearedCols = 0;    

        for (int i = 0; i < placedShape.Height; i++)
        {
            LineResult res = ScanLine(original.Row + i, false);
            if (res.IsFull)
            {
                clearPositions.UnionWith(res.Positions);
                clearedRows++;
            }
        }

        for (int i = 0; i < placedShape.Width; i++)
        {
            LineResult res = ScanLine(original.Column + i, true);
            if (res.IsFull)
            {
                clearPositions.UnionWith(res.Positions);
                clearedCols++;
            }
        }

        return new ScanResult(clearPositions, clearedRows, clearedCols);
    }

    public LineResult ScanLine(int lineIndex, bool IsVerticalScan)
    {
        int maxSearchIndex = IsVerticalScan ? _board.Rows : _board.Columns;
        HashSet<GridPosition> positions = new();

        for (int i = 0; i < maxSearchIndex; i++)
        {
            GridPosition newPos = IsVerticalScan 
                ? new GridPosition(i, lineIndex) 
                : new GridPosition(lineIndex, i);

            if (_board.CanPlace(newPos))
            {
                return new LineResult(0, false, null);
            }
            positions.Add(newPos);
        }

        return new LineResult(maxSearchIndex, true, positions);   
    }
}
