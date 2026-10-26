using System.Collections.Generic;

public class GameData
{
    public bool IsGameOver;
    public int Score;

    public List<GridPosition> OccupiedPositions;
    public List<Shape> ShapeOnTray;

    public GameData()
    {
        IsGameOver = false;
        Score = 0;
        OccupiedPositions = new List<GridPosition>();
        ShapeOnTray = new List<Shape>();
    }
}

public class ConfigData
{
    public bool IsSoundOn;
    public bool IsMusicOn;

    public ConfigData()
    {
        IsSoundOn = true;
        IsMusicOn = true;
    }
}

