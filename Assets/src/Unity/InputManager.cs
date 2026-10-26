using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public event Action<PointerEventData> OnStartDrag;
    public event Action<PointerEventData> OnDrag;
    public event Action<PointerEventData> OnEndDrag;

    private Camera _camera;

    private void Awake()
    {
        Instance = this;
    }

    public Vector3 GetMousePosition(Vector2 screenPos)
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }

        Vector3 mousePos = _camera.ScreenToWorldPoint(screenPos);
        mousePos.z = 0;

        return mousePos;
    }

    public void StartDrag(PointerEventData data)
    {
        OnStartDrag?.Invoke(data);
    }    
    
    public void Drag(PointerEventData data)
    {
        OnDrag?.Invoke(data);
    }    
    
    public void EndDrag(PointerEventData data)
    {
        OnEndDrag?.Invoke(data);
    }
}
