using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public abstract class Transition : MonoBehaviour
{
    protected RectTransform _recTransform;

    protected virtual void Awake()
    {
        _recTransform = GetComponent<RectTransform>();
    }

    public abstract UniTask TransitionIn();
    public abstract UniTask TransitionOut();
}
