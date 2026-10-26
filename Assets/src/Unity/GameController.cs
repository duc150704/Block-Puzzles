using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private DragManager _objectManager;
    private Tray _tray;
    private BoardController _boardController;

    public void Initalize(BoardController board, Tray tray, DragManager objMng)
    {
        _boardController = board;
        _objectManager = objMng;
        _tray = tray;
    }

    private void OnEnable()
    {
        DragManager.OnPlaceObject += HandlePlaceObject;
        DragManager.OnHoverObject += HandleHoverObject;
    }

    private void OnDisable()
    {
        DragManager.OnPlaceObject -= HandlePlaceObject;
        DragManager.OnHoverObject -= HandleHoverObject;
    }

    private void HandleHoverObject(Vector3 originalPos, ShapeView view)
    {
        _boardController.TryPreview(originalPos, view);   
    }

    private async void HandlePlaceObject(Vector3 originPos, ShapeView shapeView)
    {
        if (_boardController.TryPlace(originPos, shapeView))
        {
            _tray.Remove(shapeView);
            ScanResult scanResult = await _boardController.ScanBoard(originPos, shapeView.Shape);
            GameManager.Instance.Score.OnPlaceObject(shapeView.Shape.Cells.Count, scanResult);
        }
        else
        {
            _objectManager.BackToTray(shapeView);
        }

        if (IsFullBoard())
            GameManager.Instance.GameOver();
    }

    private bool IsFullBoard()
    {
        foreach (var item in _tray.ShapeViews())
        {
            if (item == null)
                continue;

            if (_boardController.HasEnoughSpaceFor(item.Shape))
                return false;
            else
                continue;
        }
        return true;
    }
}
