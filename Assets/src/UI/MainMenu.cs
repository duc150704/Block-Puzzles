using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingButton;

    [SerializeField] private SettingPanel _settingPanel;

    private RectTranformAnimator _playButtonAnimator;
    private RectTranformAnimator _settingButtonAnimator;


    private void Awake()
    {
        _playButtonAnimator = _playButton.AddComponent<RectTranformAnimator>();
        _settingButtonAnimator = _settingButton.AddComponent<RectTranformAnimator>();
    }

    private void OnEnable()
    {
        _settingButton.onClick.AddListener(OnSettingButtonClick);
        _playButton.onClick.AddListener(OnPlayButtonClick);
        //GameStateManager.Instance.OnGameStateChanged += OnChangeStateHandler;
    }

    private void Start()
    {
        _playButtonAnimator.ScaleLoop(Vector3.one * 1.1f, 0.5f, DG.Tweening.Ease.InOutSine);

        SaveManager.Instance.LoadConfig();
    }

    private void OnDisable()
    {
        _settingButton.onClick.RemoveListener(OnSettingButtonClick);
        _playButton.onClick.RemoveListener(OnPlayButtonClick);
        //GameStateManager.Instance.OnGameStateChanged -= OnChangeStateHandler;
    }

    private void OnSettingButtonClick()
    {
        _settingPanel.Show();
    }

    private void OnPlayButtonClick()
    {
        SceneController.Instance.LoadSceneAsync(SceneController.EScene.PlayScene).Forget();
    }

    //private void OnChangeStateHandler(GameStateManager.GameState state)
    //{
    //    if (state != GameStateManager.GameState.Menu)
    //        return;
        
    //}
}
