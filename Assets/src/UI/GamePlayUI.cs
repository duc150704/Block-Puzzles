using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    [Header("Pause Button")]
    [SerializeField] private Button _pauseButton;

    [Header("Pause Menu")]
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _homeButton;

    [Header("Panel")]
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private SettingPanel _settingPanel;


    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(OnPauseClick);
        _resumeButton.onClick.AddListener(OnResumeClick);
        _newGameButton.onClick.AddListener(OnNewGameClick);
        _settingButton.onClick.AddListener(OnSettingButtonClick);
        _homeButton.onClick.AddListener(OnHomeButtonClick);

        _settingPanel.OnPanelClose += OnExitSettingButtonClick;
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(OnPauseClick);
        _resumeButton.onClick.RemoveListener(OnResumeClick);
        _newGameButton.onClick.RemoveListener(OnNewGameClick);
        _settingButton.onClick.RemoveListener(OnSettingButtonClick);
        _homeButton.onClick.RemoveListener(OnHomeButtonClick);

        _settingPanel.OnPanelClose -= OnExitSettingButtonClick;
    }

    private void OnHomeButtonClick()
    {
        SceneController.Instance.LoadSceneAsync(SceneController.EScene.MenuScene).Forget();
    }

    private void OnPauseClick()
    {
        ShowPausePanel();
    }

    private void OnResumeClick()
    {
        HidePausePanel();
    }

    private void OnNewGameClick()
    {
        GameManager.Instance.Replay();
        HidePausePanel();
    }

    private void OnSettingButtonClick()
    {
        _settingPanel.Show();
        HidePausePanel();
    }

    private void OnExitSettingButtonClick()
    {
        ShowPausePanel();
    }

    private void ShowPausePanel()
    {
        _pausePanel.SetActive(true);
    }

    private void HidePausePanel()
    {
        _pausePanel.SetActive(false);
    }
}
