using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour, ISaveable
{
    [SerializeField] private ShapeSpawner _spawner;
    [SerializeField] private Transform[] _slots;

    private ShapeView[] _currentObjs;
    private Dictionary<ShapeView, int> _viewToIndexOnTray = new();

    private void Awake()
    {
        _currentObjs = new ShapeView[_slots.Length];
        GameDataManager.Register(this);
    }

    private void OnDisable()
    {
        GameDataManager.UnRegister(this);
    }

    public IEnumerable<ShapeView> ShapeViews()
    {
        for (int i = 0; i < _currentObjs.Length; i++)
            yield return _currentObjs[i];
    }
     
    private void SpawnAt(int index, Shape shape = null)
    {
        ShapeView shapeView = null; ;
        if (shape == null)
            shapeView = _spawner.CreateShape();
        else
            shapeView = _spawner.CreateShape(shape); ;

        shapeView.ReScale(0.5f);
        if (_slots[index] == null)
            Debug.Log(index + " null");
        shapeView.SetPosition(_slots[index].position);
        shapeView.OriginalPosition = _slots[index].position;

        _currentObjs[index] = shapeView;
        _viewToIndexOnTray[shapeView] = index; 
    }

    public bool IsEmpty()
    {
        bool isEmpty = true;
        for (int i = 0; i < _currentObjs.Length; i++)
        {
            if (_currentObjs[i] != null)
                isEmpty = false;
        }
        return isEmpty;
    }

    public void Remove(ShapeView obj)
    {
        int objIndex = _viewToIndexOnTray[obj];
        _currentObjs[objIndex] = null;
        _viewToIndexOnTray.Remove(obj);

        Destroy(obj.gameObject);

        if (IsEmpty())
            Refill();
    }

    private void Refill()
    {
        for(int i = 0; i < _slots.Length; i++)
        {
            SpawnAt(i);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var slot in _slots)
        {
            Gizmos.DrawSphere(slot.position, 0.2f);
        }
    }

    public void SaveGame(GameData data)
    {
        List<Shape> shapes = new List<Shape>();

        for (int i = 0; i < _currentObjs.Length; i++)
        {
            if (_currentObjs[i] == null)
                shapes.Add(null);
            else
                shapes.Add(_currentObjs[i].Shape);
        }

        data.ShapeOnTray = shapes;
    }

    public void LoadGame(GameData data)
    {
        Clear();
        List<Shape> shapes = data.ShapeOnTray;

        bool hasShape = false;
        foreach (var item in shapes)
        {
            if (item.CellSize != 0) // tam
                hasShape = true;
        }

        if (!hasShape)
        {
            Refill();
            return;
        }

        for (int i = 0; i < shapes.Count; i++)
        {
            if (shapes[i].CellSize == 0) // tam
                continue;

            SpawnAt(i, shapes[i]);
        }
    }

    private void Clear()
    {
        for (int i = 0; i < _currentObjs.Length; i++)
        {
            if (_currentObjs[i] != null)
                Destroy(_currentObjs[i].gameObject);
            _currentObjs[i] = null;
        }
    }
}
