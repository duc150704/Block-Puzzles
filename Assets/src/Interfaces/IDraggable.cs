using UnityEngine;

public interface IDraggable
{
    public Transform Transform { get; }

    public void OnStartDrag(Vector3 position);
    public void OnDrag(Vector3 position);
    public void OnEndDrag(Vector3 position);
}
