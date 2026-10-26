using System;

public class ScoreManager : ISaveable
{

    public static int HightScore { get; private set; }

    public event Action<int> OnScoreChanged;
    private int _score = 0;

    public int Score
    {
        get => _score;
        private set
        {
            _score = value;
            OnScoreChanged?.Invoke(_score);
        }
    }

    public ScoreManager()
    {
        GameDataManager.Register(this);
    }

    public void OnPlaceObject(int shapeCellCount, ScanResult scanResult)
    {
        if (scanResult.HasFullLine)
            Score += shapeCellCount + shapeCellCount * (scanResult.ClearedColumns + scanResult.ClearedRows);
        else
            Score += shapeCellCount;

        if (Score > HightScore)
            HightScore = Score;

    }

    public void SaveGame(GameData data)
    {
        data.Score = Score;
    }

    public void LoadGame(GameData data)
    {
        Score = data.Score;
    }
}
