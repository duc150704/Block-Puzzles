using System.Collections.Generic;

public struct ScanResult
{
    public HashSet<GridPosition> ClearedPositions;
    public int ClearedRows;
    public int ClearedColumns;

    public bool HasFullLine => ClearedPositions != null && ClearedPositions.Count > 0;

    public ScanResult(HashSet<GridPosition> clearedPositions, int clearedRows, int clearedColumns)
    {
        ClearedPositions = clearedPositions;
        ClearedRows = clearedRows;
        ClearedColumns = clearedColumns;
    }
}

public struct LineResult 
{
    public int Count;
    public bool IsFull;
    public HashSet<GridPosition> Positions;

    public LineResult(int count, bool isFull, HashSet<GridPosition> positions)
    {
        Count = count;
        IsFull = isFull;
        Positions = positions;
    }
}

