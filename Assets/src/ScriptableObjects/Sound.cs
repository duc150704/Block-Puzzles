using UnityEngine;

[CreateAssetMenu()]
public class Sound : ScriptableObject
{
    public ESoundType Type;
    public AudioClip Clip;
    public bool IsBackgroundMusic;
}
