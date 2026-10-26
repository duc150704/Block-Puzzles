using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragManager : MonoBehaviour
{
    public static event Action<Vector3, ShapeView> OnPlaceObject;
    public static event Action<Vector3, ShapeView> OnHoverObject;

    [SerializeField] private DragObjectData _dragObjectData;

    private IDraggable _draggable;
    private ShapeView _shapeView;

    private void OnEnable()
    {
        InputManager.Instance.OnStartDrag += OnStartDragHandler;
        InputManager.Instance.OnDrag += OnDragHandler;
        InputManager.Instance.OnEndDrag += OnEndDragHandler;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnStartDrag -= OnStartDragHandler;
        InputManager.Instance.OnDrag -= OnDragHandler;
        InputManager.Instance.OnEndDrag -= OnEndDragHandler;
    }

    public void BackToTray(ShapeView shapeView)
    {
        shapeView.ReScale(_dragObjectData.OnTrayObjectScale);
        shapeView.SetPosition(shapeView.OriginalPosition);
    }

    private void OnStartDragHandler(PointerEventData data)
    {
        _draggable = data.pointerDrag.GetComponent<IDraggable>();
        _shapeView = _draggable.Transform.GetComponent<ShapeView>();

        if (_draggable == null || _shapeView == null)
            return;

        _shapeView.ReScale(_dragObjectData.OnBoardObjectScale);

        Vector3 mousePos = InputManager.Instance.GetMousePosition(data.position);
        _draggable.OnStartDrag(_dragObjectData.Offset + mousePos);
    }

    private void OnDragHandler(PointerEventData data)
    {
        if (_draggable == null || _shapeView == null)
            return;

        Vector3 mousePos = InputManager.Instance.GetMousePosition(data.position);
        Vector3 objPosition = _dragObjectData.Offset + mousePos;

        _draggable.OnDrag(objPosition);

        OnHoverObject?.Invoke(objPosition, _shapeView);
    }

    private void OnEndDragHandler(PointerEventData data)
    {
        if (_draggable == null || _shapeView == null)
            return;

        Vector3 mousePos = InputManager.Instance.GetMousePosition(data.position);
        Vector3 objPos = _dragObjectData.Offset + mousePos;

        _draggable.OnEndDrag(objPos);

        OnPlaceObject?.Invoke(objPos, _shapeView);
        _draggable = null;
        _shapeView = null;
    }
}
