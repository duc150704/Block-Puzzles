using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance {  get; private set; }

    [SerializeField] private string _dataFileName;
    [SerializeField] private string _configFileName;

    private FileSaveHandler _fileHandler;
    private GameDataManager _dataManager;
    private GameConfigManager _configManager;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;    
        _fileHandler = new FileSaveHandler(Application.persistentDataPath);
        _configManager = new GameConfigManager();
        _dataManager = new GameDataManager();
    }

    public void NewGame()
    {
        GameData data = new GameData();
        _dataManager.Apply(data);
    }

    public void SaveData()
    {
        GameData data = new GameData();
        _dataManager.Collect(data);
        _fileHandler.Save<GameData>(data, _dataFileName);
    }

    public void SaveConfig()
    {
        ConfigData data = new ConfigData();
        _configManager.Collect(data);
        _fileHandler.Save<ConfigData>(data, _configFileName);
    }

    public GameData GetData()
    {
        return _fileHandler.Load<GameData>(_dataFileName);
    }

    public void LoadData()
    {
        GameData data = _fileHandler.Load<GameData>(_dataFileName);
        if (data == null)
            data = new GameData();

        _dataManager.Apply(data);
    }

    public void LoadConfig()
    {
        ConfigData data = _fileHandler.Load<ConfigData>(_configFileName);
        if (data == null)
            data = new ConfigData();

        _configManager.Apply(data);
    }
}
