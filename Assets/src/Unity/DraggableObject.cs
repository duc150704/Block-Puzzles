using UnityEngine;

public class DraggableObject : MonoBehaviour, IDraggable
{
    public Transform Transform => transform;

    public void OnDrag(Vector3 position)
    {
        this.Transform.position = position;
    }

    public void OnEndDrag(Vector3 position)
    {
        this.Transform.position = position;
    }

    public void OnStartDrag(Vector3 position)
    {
        this.Transform.position = position;
    }
}
