using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] private GameObject _pref;
    [SerializeField] private float _cellSize;
    [SerializeField] private Transform _dropZone;

    private CellView[,] _cellViews;
    private CoordinateConverter _converter;
    private int _rows;
    private int _columns;

    private void Update()
    {
        //Showdebug();
    }

    public void Initialize(int rows, int columns)
    {
        _rows = rows;
        _columns = columns;
        _cellViews = new CellView[rows, columns]; 

        Vector3 translationVector = GetTranslationVector();
        _converter = new CoordinateConverter(_cellSize, translationVector);

        for (int x = 0; x < _columns; x++)
        {
            for (int y = 0; y < _rows; y++)
            {
                Vector3 position = GridToWorld(y, x);
                GameObject newGo = Instantiate(_pref, position, Quaternion.identity);
                _cellViews[y, x] = newGo.GetComponent<CellView>();

                _cellViews[y, x].SetSpriteLayer("Board", 1);
                newGo.SetActive(false);
                newGo.transform.SetParent(this.transform);
            }
        }
    }

    public void Occupy(HashSet<GridPosition> gridPositions)
    {
        foreach (var pos in gridPositions)
        {
            Occupy(pos);
        }
    }

    public void Occupy(GridPosition gridposition)
    {
        _cellViews[gridposition.Row, gridposition.Column].gameObject.SetActive(true);
    }

    public async UniTask Free(HashSet<GridPosition> gridPositions)
    {
        List<UniTask> tasks = new();
        foreach (var pos in gridPositions)
        {
            tasks.Add(Free(pos.Row, pos.Column));
        }

        await UniTask.WhenAll(tasks);
    }

    public async UniTask Free(int  row, int column)
    {
        Vector3 oldScale = _cellViews[row, column].transform.localScale;
        await _cellViews[row, column].Scale(Vector3.zero, 0.2f, Ease.InBack).ToUniTask();
        _cellViews[row, column].gameObject.SetActive(false);
        await _cellViews[row, column].Scale(oldScale, 0, Ease.Flash);
    }

    public GridPosition WorldToGrid(Vector3 position)
        => _converter.WorldToGrid(position);

    public Vector3 GridToWorld(int row, int columns)
        =>_converter.GridToWorld(row, columns);

    private Vector3 GetTranslationVector()
    {
        Vector3 centerBoard = (Vector3.zero + new Vector3(_columns, _rows)) * _cellSize / 2;
        return _dropZone.position - centerBoard;
    }

    private void Showdebug()
    {
        for (int i = 0; i <= _rows; i++)
        {
            Debug.DrawLine(
                _converter.GridToWorld(i, 0) - new Vector3(_cellSize / 2, _cellSize / 2),
                _converter.GridToWorld(i, _columns) - new Vector3(_cellSize / 2, _cellSize / 2),
                Color.red
                );
        }
        for (int i = 0; i <= _columns; i++)
        {
            Debug.DrawLine(
                _converter.GridToWorld(0, i) - new Vector3(_cellSize / 2, _cellSize / 2),
                _converter.GridToWorld(_rows, i) - new Vector3(_cellSize / 2, _cellSize / 2),
                Color.red
                );
        }
    }
}
