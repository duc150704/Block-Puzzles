using System;

[Serializable]
public struct GridPosition
{
    public int Row;
    public int Column;

    public GridPosition(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public override string ToString()
    {
        return $"{Row} , {Column}";
    }

    public static GridPosition operator +(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.Row + b.Row, a.Column + b.Column) ;
    }
}
