using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class DragEvents : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Collider2D _col;

    private void Awake()
    {
        _col = GetComponent<Collider2D>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        InputManager.Instance.StartDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        InputManager.Instance.Drag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        InputManager.Instance.EndDrag(eventData);
    }
}
