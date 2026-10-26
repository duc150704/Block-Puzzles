using System.Collections.Generic;
using UnityEngine;

public enum ESoundType
{
    Background, PlaceShape, ClearCell, CantPlace
}

public class SoundManager : MonoBehaviour, IConfigable
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource _bgMusic;
    [SerializeField] private AudioSource _sef;
    [SerializeField] private List<Sound> _soundData = new();

    private Dictionary<ESoundType, Sound> _soundMap = new();

    public bool IsSoundOn => !_sef.mute;
    public bool IsMusicOn => !_bgMusic.mute;

    private void Awake()
    {
        if (Instance != null) 
        {
            Destroy(this.gameObject);
            return;
        }
            
        Instance = this;
        GameConfigManager.Register(this);

        foreach (var item in _soundData)
        {
            _soundMap[item.Type] = item;
        }
    }

    public void Play(ESoundType sound)
    {
        if (_soundMap[sound].IsBackgroundMusic == true)
            PlayBackground(sound);
        else
            PlaySound(sound);
    }

    private void PlaySound(ESoundType sound)
    {
        _sef.clip = _soundMap[sound].Clip;
        _sef.PlayOneShot(_sef.clip);
    }

    private void PlayBackground(ESoundType sound)
    {
        _bgMusic.clip = _soundMap[sound].Clip;
        _bgMusic.Play();
    }

    public void SetSound(bool isOn)
        => _sef.mute = isOn ? false : true;

    public void SetMusic(bool isOn)
        => _bgMusic.mute = isOn ? false : true;

    public void Save(ConfigData data)
    {
        data.IsSoundOn = !_sef.mute;
        data.IsMusicOn = !_bgMusic.mute;
    }

    public void Load(ConfigData data)
    {
        _sef.mute = !data.IsSoundOn;
        _bgMusic.mute= !data.IsMusicOn;
    }
}
