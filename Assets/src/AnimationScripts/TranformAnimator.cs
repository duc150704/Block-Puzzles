using DG.Tweening;
using UnityEngine;

public class TranformAnimator : MonoBehaviour
{
    public Tween Scale(Vector3 scale, float time, Ease ease)
    {
        return transform.DOScale(scale, time).SetEase(ease);
    }
}
