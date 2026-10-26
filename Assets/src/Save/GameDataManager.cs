using System.Collections.Generic;

public class GameDataManager
{
    private static List<ISaveable> _saveables = new List<ISaveable>();

    public void Collect(GameData data)
    {
        foreach (var item in _saveables)
        {
            item.SaveGame(data);
        }
    }

    public void Apply(GameData data) 
    {
        foreach (var item in _saveables)
        {
            item.LoadGame(data);
        }
    }

    public static void Register(ISaveable saveable)
    {
        if (!_saveables.Contains(saveable))
            _saveables.Add(saveable);
    }

    public static void UnRegister(ISaveable saveable)
    {
        _saveables.Remove(saveable);
    }
}
