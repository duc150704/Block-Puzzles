public interface ISaveable
{
    public void SaveGame(GameData data);
    public void LoadGame(GameData data);
}

public interface IConfigable
{
    public void Save(ConfigData data);
    public void Load(ConfigData data);
}
