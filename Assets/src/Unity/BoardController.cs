using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    private int _rows;
    private int _columns;
    private BoardView _view;

    private Board _board;
    private BlashLogic _blashLogic;


    private void OnEnable()
    {
        GameManager.Instance.OnPlay += Load;
        GameManager.Instance.OnReplay += Load;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlay -= Load;
        GameManager.Instance.OnReplay -= Load;
    }

    public void Initalize(int rows, int columns, BoardView view)
    {
        _rows = rows;
        _columns = columns;
        _view = view;

        _board = new Board(_rows, _columns);
        _blashLogic = new BlashLogic(_board);
        _view.Initialize(_rows, _columns);
    }

    public void Load()
    {
        foreach (var item in _board.Values())
        {
            if (!item.IsEmpty)
                _view.Occupy(item.GridPosition);
            else
                _view.Free(item.GridPosition.Row, item.GridPosition.Column).Forget();
        }
    }

    public bool HasEnoughSpaceFor(Shape shape)
    {
        return _blashLogic.HasEnoughSpaceFor(shape.Cells.ToHashSet());
    }

    public void TryPreview(Vector3 originalPosition, ShapeView shape)
    {
        HashSet<GridPosition> ocuppyPos = Convert(originalPosition, shape.Shape.Cells);
        if (!_board.CanPlace(ocuppyPos))
        {
            shape.HidePreview();
            return;
        }

        GridPosition pos = _view.WorldToGrid(originalPosition); 
        shape.ShowPreview(_view.GridToWorld(pos.Row, pos.Column));
    }

    public bool TryPlace(Vector3 originalPosition, ShapeView shape)
    {
        HashSet<GridPosition> ocuppyPos = Convert(originalPosition, shape.Shape.Cells);
        if (!_board.CanPlace(ocuppyPos))
        {
            SoundManager.Instance.Play(ESoundType.CantPlace);
            return false;
        }
            

        SoundManager.Instance.Play(ESoundType.PlaceShape);
        _board.Occupy(ocuppyPos);
        _view.Occupy(ocuppyPos);
        return true;
    }

    public async UniTask<ScanResult> ScanBoard(Vector3 originalPosition, Shape shape)
    {
        GridPosition originalGridPos = _view.WorldToGrid(originalPosition);

        ScanResult res = _blashLogic.Scan(originalGridPos, shape);
        if (!res.HasFullLine)
            return res;

        SoundManager.Instance.Play(ESoundType.ClearCell);
        _board.Free(res.ClearedPositions);
        await _view.Free(res.ClearedPositions);
        return res;
    }

    public HashSet<GridPosition> Convert(Vector3 originalPosition, List<GridPosition> gridPositions)
    {
        GridPosition originPositon = _view.WorldToGrid(originalPosition);
        HashSet<GridPosition> occupyPositions = new();
        foreach (var item in gridPositions)
        {
             occupyPositions.Add(originPositon + item);
        }
        return occupyPositions;
    }

}
