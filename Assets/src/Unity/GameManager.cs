using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private BoardController _boardController;
    [SerializeField] private GameController _gameController;
    [SerializeField] private Tray _tray;
    [SerializeField] private DragManager _draggableObjectsManager;
    [SerializeField] private BoardView _boardView;
    [SerializeField] private GameOverPanel _gameOverPanel;

    [Header("Board Infomation")]
    [SerializeField] private int Rows;
    [SerializeField] private int Columns;

    private ScoreManager _scoreManager;
    private FileSaveHandler _dataHandler;
    private ConfigData _configData;
    private GameData _gameData;

    private GameDataManager _dataManager;
    private GameConfigManager _configManager;

    public ScoreManager Score => _scoreManager;
    public BoardController BoardController => _boardController;

    public event Action OnPlay;
    public event Action OnReplay;
    public event Action OnGameOver;

    private void Awake()
    {
        Instance = this;
        _scoreManager = new ScoreManager();
        _dataHandler = new FileSaveHandler(Application.persistentDataPath);

        _configManager = new GameConfigManager();
        _dataManager = new GameDataManager();
    }

    private void Start()
    {
        _boardController.Initalize(Rows, Columns, _boardView);
        _gameController.Initalize(_boardController, _tray, _draggableObjectsManager);

        //OnOpenGame();
        Play();
    }

    private void OnEnable()
    {
        OnGameOver += _gameOverPanel.Show;
    }

    private void OnDisable()
    {
        OnGameOver -= _gameOverPanel.Show;
    }

    private void OnApplicationQuit()
    {
        SaveManager.Instance.SaveConfig();
        SaveManager.Instance.SaveData();
    }

    public void GameOver()
    {
        _gameOverPanel.SetScore(_scoreManager.Score);
        SaveManager.Instance.SaveData();
        OnGameOver?.Invoke();
    }

    public void Replay()
    {
        SaveManager.Instance.NewGame();
        SaveManager.Instance.SaveData();
        OnReplay?.Invoke();
    }

    public void Play()
    {
        GameData data = SaveManager.Instance.GetData();
        if(data.IsGameOver)
            SaveManager.Instance.NewGame();
        else
            SaveManager.Instance.LoadData();

        OnPlay?.Invoke();
    }
}
