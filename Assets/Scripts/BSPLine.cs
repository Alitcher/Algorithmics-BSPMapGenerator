using UnityEngine;
public class BSPLine
{
    Orientation orientation;
    Vector2Int coordinates;

    public BSPLine(Orientation orientation, Vector2Int coordinates)
    {
        this.orientation = orientation;
        this.coordinates = coordinates;
    }

    public Orientation Orientation { get => orientation; set => orientation = value; }
    public Vector2Int Coordinates { get => coordinates; set => coordinates = value; }
}

public enum Orientation
{
    Landscape = 0,
    Portrait = 1
}