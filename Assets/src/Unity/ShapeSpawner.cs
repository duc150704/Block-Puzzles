using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _shapeViewPref;
    [SerializeField] private List<ShapeData> _shapeData;
    [SerializeField] private List<Sprite> _cellSprite;

    private ShapeFactory _shapeFactory ;

    public List<ShapeData> ShapeData => _shapeData;

    public ShapeView CreateShape()
    {
        ShapeData shapeData = GetRamdomData();

        if (_shapeFactory == null)
            _shapeFactory = new ShapeFactory(_shapeData);

        Shape shape = _shapeFactory.Create(shapeData.ShapeType, DirectionHelper.GetRandomDirection());
        ShapeView shapeView = ShapeView.Create(_shapeViewPref);
        shapeView.Create(shape);
        shapeView.SetSprite(GetRandomSprite());

        return shapeView;
    }
    
    public ShapeView CreateShape(Shape shape)
    {
        if (_shapeFactory == null)
            _shapeFactory = new ShapeFactory(_shapeData);

        shape = _shapeFactory.Create(shape.ShapeType, shape.Direction);
        ShapeView shapeView = ShapeView.Create(_shapeViewPref);
        shapeView.Create(shape);
        shapeView.SetSprite(GetRandomSprite());

        return shapeView;
    }

    private Sprite GetRandomSprite()
    {
        int value = Random.Range(0, _cellSprite.Count);
        return _cellSprite[value];
    }

    public ShapeData GetRamdomData()
    {
        int value = Random.Range(0, _shapeData.Count);
        return _shapeData[value];
    }
}
