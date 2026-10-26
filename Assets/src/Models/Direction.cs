using System;

public enum Direction
{
    Up, Down,
    Left, Right
}

public static class DirectionHelper
{
    public static Direction GetRandomDirection()
    {
        Direction dir = Direction.Up;
        int value = UnityEngine.Random.Range(0, 4);

        switch (value)
        {
            case 0:
                dir = Direction.Up;
                break;
            case 1:
                dir = Direction.Right;
                break;
            case 2:
                dir = Direction.Down;
                break;
            case 3:
                dir = Direction.Left;
                break;
        }
        return dir;
    }
}
