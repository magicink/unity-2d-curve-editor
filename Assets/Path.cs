using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Path
{
    [SerializeField] [HideInInspector] private List<Vector2> points;

    public int TotalPoints => points.Count;
    public int TotalSegments => points.Count % 3;
    public Vector2 this[int i] => points[i];

    public Path(Vector2 center)
    {
        points = new List<Vector2>
        {
            center + Vector2.left,
            center + (Vector2.left + Vector2.up) * 0.5f,
            center + (Vector2.right + Vector2.down) * 0.5f,
            center + Vector2.right
        };
    }

    private void AddPoint(Vector2 point)
    {
        points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
        points.Add((points[points.Count - 1] + point) * 0.5f);
        points.Add(point);
    }

    public Vector2[] GetPointsInSegment(int i)
    {
        var result = new Vector2[4];
        var pointList = points.ToArray();
        Array.Copy(pointList, i * 3, result, 0, 4);
        return result;
    }

    public void MovePoint(int i, Vector2 position)
    {
        points[i] = position;
    }
}
