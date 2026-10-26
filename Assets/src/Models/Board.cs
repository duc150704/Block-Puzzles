using System.Collections.Generic;

public class Board : ISaveable
{
    public int Rows => _grid.Rows;
    public int Columns => _grid.Columns;

    private Grid2D<BoardCell> _grid;

    public Board(int rows, int columns)
    {
        _grid = new Grid2D<BoardCell>(rows, columns);
        InitializeCell();
        GameDataManager.Register(this);
    }

    public bool CanPlace(HashSet<GridPosition> gridPositions)
    {
        foreach (var item in gridPositions)
        {
            if (!CanPlace(item))
                return false;
        }
        return true;
    }

    public void Occupy(HashSet<GridPosition> gridPositions)
    {
        foreach (var pos in gridPositions)
        {
            Occupy(pos.Row, pos.Column);
        }
    }

    public void Free(HashSet<GridPosition> gridPositions)
    {
        foreach (var item in gridPositions)
        {
            Free(item.Row, item.Column);
        }
    }

    public bool CanPlace(GridPosition gridPosition)
    {
        if (!_grid.IsValid(gridPosition))
            return false;

        BoardCell cell = _grid.Get(gridPosition);
        return cell != null && cell.IsEmpty;
    }

    public void Occupy(int row, int column)
    {
        GridPosition gridPosition = new GridPosition(row, column);
        BoardCell cell = _grid.Get(gridPosition);
        cell.Occupy();
    }

    public bool Free(int row, int column)
    {
        GridPosition gridPosition = new GridPosition(row, column);
        BoardCell cell = _grid.Get(gridPosition);
        cell.Free();
        return true;
    }

    public IEnumerable<GridPosition> Positions()
    {
        for (int c = 0; c < Columns; c++)
        {
            for (int r = 0; r < Rows; r++)
            {
                yield return new GridPosition(r, c);
            }
        }
    }

    public IEnumerable<BoardCell> Values()
    {
        for (int c = 0; c < Columns; c++)
        {
            for (int r = 0; r < Rows; r++)
            {
                yield return _grid.Get(new GridPosition(r, c));
            }
        }
    }

    private void InitializeCell()
    {
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Columns; c++)
            {
                GridPosition gridPosition = new GridPosition(r, c);
                _grid.Set(gridPosition, new BoardCell(gridPosition));
            }
        }
    }

    public void SaveGame(GameData data)
    {
        List<GridPosition> occupiedPositions = new();
        foreach (var item in Positions())
        {
            BoardCell cell = _grid.Get(item);
            if (cell != null && !cell.IsEmpty)
            {
                occupiedPositions.Add(item);
            }
        }

        data.OccupiedPositions = occupiedPositions;
    }

    public void LoadGame(GameData data)
    {
        foreach (var item in Values())
        {
            item.Free();
        }

        foreach (var item in data.OccupiedPositions)
        {
            Occupy(item.Row, item.Column);
        }
    }
}
