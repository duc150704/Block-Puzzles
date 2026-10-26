using Cysharp.Threading.Tasks;
using DG.Tweening;

public class SquareTransition : Transition
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override UniTask TransitionIn()
    {
        _recTransform.DOKill();
        return _recTransform.DOScaleY(1, 0.2f).SetEase(Ease.Linear).ToUniTask();
    }

    public override UniTask TransitionOut()
    {
        _recTransform.DOKill();
        return _recTransform.DOScaleY(0, 0.2f).SetEase(Ease.Linear).ToUniTask();
    }
}
