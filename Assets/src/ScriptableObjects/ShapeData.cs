using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeData : ScriptableObject
{
    public ShapeType ShapeType;
    public List<Vector3> Cells;

    public int Height;
    public int Width;
    public float CellSize;

}
