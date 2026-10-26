using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [Header("Toggle")]
    [SerializeField] private Toggle _soundToggle;
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Button _closeButton;

    [Header("Sprite")]
    [SerializeField] private Sprite _toogleOff;
    [SerializeField] private Sprite _toogleOn;

    public event Action OnPanelClose;

    private void OnEnable()
    { 
        _soundToggle.onValueChanged.AddListener(ClickSoundToggle);
        _musicToggle.onValueChanged.AddListener(ClickMusicToggle);
        _closeButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _soundToggle.onValueChanged.RemoveListener(ClickSoundToggle);
        _musicToggle.onValueChanged.RemoveListener(ClickMusicToggle);
        _closeButton.onClick.RemoveListener(Close);
    }

    public void Show()
    {
        gameObject.SetActive(true);

        _soundToggle.SetIsOnWithoutNotify(SoundManager.Instance.IsSoundOn);
        _musicToggle.SetIsOnWithoutNotify(SoundManager.Instance.IsMusicOn);

        UpdateSoundUI();
        UpdateMusicUI();
    }

    private void ClickSoundToggle(bool isOn)
    {
        SoundManager.Instance.SetSound(isOn);
        UpdateSoundUI();
    }

    private void ClickMusicToggle(bool isOn)
    {
        SoundManager.Instance.SetMusic(isOn);
        UpdateMusicUI();
    }

    private void Close()
    {
        gameObject.SetActive(false);
        SaveManager.Instance.SaveConfig();
        OnPanelClose?.Invoke();
    }

    private void UpdateSoundUI()
    {
        _soundToggle.image.sprite 
            = SoundManager.Instance.IsSoundOn
            ? _toogleOn
            : _toogleOff;
    }

    private void UpdateMusicUI()
    {
        _musicToggle.image.sprite
            = SoundManager.Instance.IsMusicOn
            ? _toogleOn
            : _toogleOff;
    }

}
